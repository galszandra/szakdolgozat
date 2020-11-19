using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication
{
    public static class KnapSack
    {
        public static List<Scheduler.Task> OrderTasks(List<Scheduler.Task> Tasks)
        {
            var orderedList = Tasks.OrderByDescending(task => task.TotalValue).ToList();
            return orderedList;
        }

        public static int KnapSackAlgorithm(int capacity, List<Scheduler.Task> orderedList, int n, int[] included)
        {
            if (n == 0 || capacity == 0) //if we're not using any tasks or we have no capacity, the result is 0
            {
                return 0;
            }

            //if a task's length is greater than the capacity,
            //use the best result we can get without including that task

            if (orderedList[n - 1].TaskDuration > capacity)
            {
                return KnapSackAlgorithm(capacity, orderedList, n - 1, included);
            }

            //otherwise, decide if we need that task or not
            //two versions:
            //(1) task included
            //(2) task not included

            else
            {
                int[] v1 = new int[included.Length];
                Array.Copy(included, 0, v1, 0, v1.Length);
                v1[n - 1] = 1;

                int[] v2 = new int[included.Length];
                Array.Copy(included, 0, v2, 0, v2.Length);

                int result1 = orderedList[n - 1].TaskPriority + KnapSackAlgorithm(capacity - orderedList[n - 1].TaskDuration, orderedList, n - 1, v1);

                int result2 = KnapSackAlgorithm(capacity, orderedList, n - 1, v2);

                if (result1 > result2)
                {
                    Array.Copy(v1, 0, included, 0, v1.Length);
                   return result1;
                }

                else
                {
                    Array.Copy(v2, 0, included, 0, v2.Length);
                    return result2;
                }
            }
        }
    }
}