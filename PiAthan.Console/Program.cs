using PiAthan.Services;
using Quartz;
using Quartz.Impl;

namespace PiAthan.Console
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var salahTimeService = new SalahTimeService();
            
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<ScheduleSalahTimesJob>()
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                //.WithSchedule(CronScheduleBuilder.CronSchedule("0 10 0 * * ?")) // Reschedule new prayer times every 00:10am
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).WithRepeatCount(0))
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}