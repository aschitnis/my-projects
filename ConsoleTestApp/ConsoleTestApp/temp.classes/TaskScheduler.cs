using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp.temp.classes
{
    public class TaskScheduler
    {
        private static TaskScheduler _instance;
        private List<Timer> timers = new List<Timer>();


        private TaskScheduler() { }

        public static TaskScheduler Instance => _instance ?? (_instance = new TaskScheduler());

        public void ScheduleTask(int hour, int min, double intervalInHour, Action task)
        {
            DateTime now = DateTime.Now;
            DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, hour, min, 0, 0);

            if (now > firstRun)
            {
                firstRun = firstRun.AddDays(1);
            }

            TimeSpan timeToGo = firstRun - now;
            if (timeToGo <= TimeSpan.Zero)
            {
                timeToGo = TimeSpan.Zero;
            }

            System.Threading.Timer timer = new System.Threading.Timer(x =>
            {
                task.Invoke();
            }, null, timeToGo, TimeSpan.FromHours(intervalInHour));

            timers.Add(timer);

            //System.Threading.Timer timer = new System.Threading.Timer(timer_Elapsed, null, timeToGo, TimeSpan.FromHours(intervalInHour));

            //timers.Add(timer);
        }

        void timer_Elapsed(object state)
        {
            // Thread.Sleep(100);
            // Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            timers[0].Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
