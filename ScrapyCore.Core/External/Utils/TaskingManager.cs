using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScrapyCore.Core.External.Utils
{
    public class TaskingManager
    {
        private List<Task> Tasks = new List<Task>();
        public void AddTask(Task tsk)
        {
            Tasks.Add(tsk);
        }

        public Task Wait()
        {
            return Task.WhenAll(Tasks.ToArray());
        }
    }
}
