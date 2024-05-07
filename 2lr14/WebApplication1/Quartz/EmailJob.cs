using Quartz;

namespace WebApplication1.Quartz
{
    public class EmailJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Sending email...");
            return Task.CompletedTask;
        }
    }
}
