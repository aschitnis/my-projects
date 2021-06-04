using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wpf.Test.my.weather.classes.services
{
    public class SchedulerService
    {
        private static SchedulerService _instance;
        private List<Timer> timers = new List<Timer>();
        public static SchedulerService Instance => _instance ?? (_instance = new SchedulerService());

        #region constructor
        private SchedulerService() { }
        #endregion

        public void ScheduleTaskWithInterval(int hour, int min, double intervalinminutes, double intervalinseconds, Action<string> task, string parameter)
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

            TimeSpan timeInterval = new TimeSpan();
            if (intervalinminutes > 0)
            {
                timeInterval = TimeSpan.FromMinutes(intervalinminutes);
            }
            else if (intervalinseconds > 0)
            {
                timeInterval = TimeSpan.FromSeconds(intervalinseconds);
            }

            System.Threading.Timer timer = new System.Threading.Timer(x =>
            {
                task.Invoke(parameter);
            }, null, timeToGo, timeInterval);     
            timers.Add(timer);

            //System.Threading.Timer timer = new System.Threading.Timer(timer_Elapsed, null, timeToGo, TimeSpan.FromHours(intervalInHour));

            //timers.Add(timer);
        }

        static void timer_Elapsed(object state)
        {
            // Thread.Sleep(100);
            // Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            // timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}
