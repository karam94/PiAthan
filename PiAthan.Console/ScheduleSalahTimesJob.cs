using System;
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
            var timings = salahTimeService.GetSalahTimes().GetSalahTimes();

            foreach (var salah in timings)
            {
                //DateTime time = DateTime.Now;
            
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                IScheduler scheduler = schedulerFactory.GetScheduler();
                scheduler.Start();

                IJobDetail job = JobBuilder.Create<AthanJob>()
                    .Build();

//                ITrigger trigger = TriggerBuilder.Create()
//                    .StartAt(time)
//                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .StartAt(salah.Datetime)
                    .Build();

                scheduler.ScheduleJob(job, trigger); 
            }
        }
    }
}