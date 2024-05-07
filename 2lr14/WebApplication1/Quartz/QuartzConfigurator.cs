using Quartz.Impl;
using Quartz;

namespace WebApplication1.Quartz
{
    public class QuartzConfigurator
    {
        public static async Task<IScheduler> ConfigureQuartz(IServiceProvider serviceProvider)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<EmailJob>()
                .WithIdentity("emailJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("emailTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);

            return scheduler;
        }
    }
}
