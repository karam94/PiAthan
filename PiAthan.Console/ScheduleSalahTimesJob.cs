using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
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
        private readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private IEnumerable<Salah> GetSalahTimes()
        {
            _logger.Info($"{DateTime.Now}: Getting new Salah times...)");
            return _salahTimeService.GetSalahTimes().GetSalahTimes();
        }

        public void Execute(IJobExecutionContext context)
        {
            _scheduler.Start();

            foreach (var salah in GetSalahTimes().Where(x => x.Name != "Fajr"))
            {
                var job = JobBuilder.Create<AthanJob>()
                    .Build();

                if (salah.Datetime > DateTime.Now)
                {
                    var salahTimeTrigger = TriggerBuilder.Create()
                        .StartAt(salah.Datetime)
                        .Build();

                    _scheduler.ScheduleJob(job, salahTimeTrigger);
                    _logger.Info($"Scheduled {salah.Name} at {salah.Datetime.ToShortTimeString()}");
                    System.Console.WriteLine($"Scheduled {salah.Name} at {salah.Datetime.ToShortTimeString()}");
                }
            }
        }
    }
}