using Microsoft.AspNetCore.Mvc;
using ScrapyCore.Core;
using ScrapyCore.Core.External.Utils;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.Message;
using ScrapyCore.Core.Platform.Processors;
using ScrapyCore.Fundamental.Scheduler;
using ScrapyCore.Fundamental.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapyCore.HeartOfSwarm.Controllers.Apis
{
    [Route("api/[controller]")]
    public class ScarpyController : Controller
    {
        private readonly ICache cache;
        private IMessageQueue messageQueue;
        private IPlatformExit platformExit;
        public ScarpyController(ICache cache, IMessageQueue messageQueue)
        {
            this.cache = cache;
            this.messageQueue = messageQueue;
            this.platformExit = new MessageQueuePlatformExit(messageQueue);
        }

        [Route("task-list")]
        public async Task<ActionResult> ScrapyTaskList()
        {
            List<object> result = new List<object>();
            foreach (var item in await cache.SearchKeys("msg-*"))
            {
                var indexer = cache.Restore<MessageIndexer>(item);
                result.Add(new
                {
                    Id = indexer.MessageId,
                    Name = indexer.MessageName,
                    StartTime = indexer.StartTime.ToString(),
                    SubTask = indexer.SubTask,
                    Completed = indexer.Completed
                });
            }
            return Json(result);
        }

        [HttpPost]
        [Route("new-task")]
        public async Task<ActionResult> ScrapyNewTask([FromBody]ScheduleMessage scheduleMessage)
        {
            ScheduleIntegration scheduleIntegration = new ScheduleIntegration(null, cache, platformExit);
            await scheduleIntegration.ScheduleNew(scheduleMessage);
            return Json("Scheduled finished");
        }


        [Route("block-queue")]
        [HttpGet]
        public async Task<ActionResult> SendMessageToHydalisk([FromQuery(Name = "delay")]int delay, [FromQuery(Name = "loop")]int loop)
        {
            for (int i = 0; i < loop; i++)
            {
                await messageQueue.SendQueueMessage(new PlatformMessage()
                {
                    Command = new Core.Platform.Commands.Command()
                    {
                        CommandCode = Core.Platform.Commands.CommandCode.Configure,
                        CommandType = Core.Platform.Commands.CommandTransfer.Random,
                    },
                    MessageData = BitConverter.GetBytes(delay)
                });
            }
            return Json($"Send {loop} heavy message delay {delay} to hyralisk.....");
        }

    }
}
