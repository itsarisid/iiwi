using Quartz.Spi;
using Quartz;
using Microsoft.Extensions.DependencyInjection;

namespace iiwi.Scheduler;

public class MyJobFactory(IServiceProvider container) : IJobFactory
{
    protected readonly IServiceProvider Container = container;

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler) => (IJob)Container.GetRequiredService(bundle.JobDetail.JobType);

    public void ReturnJob(IJob job) => (job as IDisposable)?.Dispose();
}