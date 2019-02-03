using System;
using System.Globalization;
using PiAthan.Domain;
using PiAthan.Services;
using Quartz;
using Quartz.Impl;

namespace PiAthan.Console
{
    public class ScheduleSalahTimesJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {            
            var salahTimeService = new SalahTimeService();
            var timings = salahTimeService.GetSalahTimes();

            DateTime time = DateTime.ParseExact(timings.Fajr, "HH:mm", CultureInfo.InvariantCulture);
            //DateTime time = DateTime.Now;
            
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<AthanJob>()
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartAt(time)
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}