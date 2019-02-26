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

            var programLaunchScheduleSalahTimesJob = JobBuilder.Create<ScheduleSalahTimesJob>()
                .Build();

            var programLaunchScheduleSalahTimesTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).WithRepeatCount(0)) // Also fetch on program load once.
                .Build();

            var dailyScheduleSalahTimesJob = JobBuilder.Create<ScheduleSalahTimesJob>()
                .Build();

            var dailyScheduleSalahTimesTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(00, 01)) // Fetch new prayer times every day at midnight.
                .Build();

            var programLoadJob = JobBuilder.Create<AthanJob>()
                .Build();

            var programLoadTrigger = TriggerBuilder.Create()
                .StartAt(DateTime.Now)
                .Build();

            Scheduler.ScheduleJob(programLaunchScheduleSalahTimesJob, programLaunchScheduleSalahTimesTrigger);
            Scheduler.ScheduleJob(dailyScheduleSalahTimesJob, dailyScheduleSalahTimesTrigger);
            Scheduler.ScheduleJob(programLoadJob, programLoadTrigger);
        }
    }
}