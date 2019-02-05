using System;
using Quartz;
using Quartz.Impl;

namespace PiAthan.Console
{
    public static class Program
    {
        private static readonly ISchedulerFactory SchedulerFactory = new StdSchedulerFactory();
        private static readonly IScheduler Scheduler = SchedulerFactory.GetScheduler();
        
        public static void Main(string[] args)
        {
            System.Console.WriteLine("PiAthan App Launched!");
            System.Console.WriteLine("http://karam.io");
            
            Scheduler.Start();

            var scheduleSalahTimesJob = JobBuilder.Create<ScheduleSalahTimesJob>()
                .Build();

            var scheduleSalahTimesTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule("0 10 0 * * ?")) // Fetch new prayer times every 00:10am.
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).WithRepeatCount(0)) // Also fetch on program load once.
                .Build();
            
            var programLoadJob = JobBuilder.Create<AthanJob>()
                .Build();
                                
            var programLoadTrigger = TriggerBuilder.Create()
                .StartAt(DateTime.Now)
                .Build();

            Scheduler.ScheduleJob(scheduleSalahTimesJob, scheduleSalahTimesTrigger);
            Scheduler.ScheduleJob(programLoadJob, programLoadTrigger);
        }
    }
}