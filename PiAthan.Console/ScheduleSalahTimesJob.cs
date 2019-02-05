using System;
using System.Collections.Generic;
using PiAthan.Domain;
using PiAthan.Services;
using Quartz;
using Quartz.Impl;

namespace PiAthan.Console
{
    public class ScheduleSalahTimesJob : IJob
    {
        private static readonly ISchedulerFactory SchedulerFactory = new StdSchedulerFactory();
        private readonly IScheduler _scheduler = SchedulerFactory.GetScheduler();
        private readonly SalahTimeService _salahTimeService = new SalahTimeService();

        private IEnumerable<Salah> GetSalahTimes()
        {
            return _salahTimeService.GetSalahTimes().GetSalahTimes(); 
        }
        
        public void Execute(IJobExecutionContext context)
        {
            _scheduler.Start();

            foreach (var salah in GetSalahTimes())
            {
                var job = JobBuilder.Create<AthanJob>()
                    .Build();
                
                if(salah.Datetime > DateTime.Now){
                    var salahTimeTrigger = TriggerBuilder.Create()
                        .StartAt(salah.Datetime)
                        .Build();
    
                    _scheduler.ScheduleJob(job, salahTimeTrigger);
                    System.Console.WriteLine($"Scheduled {salah.Name} at {salah.Datetime.ToShortTimeString()}");
                }
            }
        }
    }
}