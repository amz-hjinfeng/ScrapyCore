using Microsoft.AspNetCore.Mvc;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.Commands;
using ScrapyCore.Core.Platform.Message;
using System;

namespace ScrapyCore.Apis
{
    [Route("api/[controller]")]
    public class SiteToSiteJumpController : Controller
    {
        private readonly ICache cache;
        private readonly IMessageEntrance messageEntrance;

        public SiteToSiteJumpController(ICache cache, IMessageEntrance messageEntrance)
        {
            this.cache = cache;
            this.messageEntrance = messageEntrance;
        }

        [HttpGet]
        public string GetValue()
        {
            return "It is OK";
        }

        [HttpPost]
        public bool PostValue([FromBody]PlatformMessage platformMessage,
            [FromHeader(Name = "x-principal")]string princpal,
            [FromHeader(Name = "x-principal-id")]string princpalId)
        {

            if (princpal == platformMessage.NextJump.IpAddress.ToString() && princpalId == platformMessage.NextJump.Id)
            {
                if (princpal == Request.Host.Host)
                {
                    ///The siteToSite Command should be processed in this jump.
                    platformMessage.Command.CommandType = CommandTransfer.Random;
                    messageEntrance.PushMessageBySiteToSiteCommand(platformMessage);
                }
            }
            return true;
        }
    }
}
