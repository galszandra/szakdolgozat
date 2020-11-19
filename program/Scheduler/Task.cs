using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Task
    {
        //A Task osztály konstruktora
        public Task(string title, int priority, int duration)
        {
            TaskTitle = title;
            TaskPriority = priority;
            TaskDuration = duration;
            TotalValue = (double)priority / (double)duration;
        }

        public string TaskTitle { get; set; }
        public int TaskPriority { get; set; }
        public int TaskDuration { get; set; }
        public double TotalValue { get; set; }
    }
}