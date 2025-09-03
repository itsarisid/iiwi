using Quartz.Spi;
using Quartz;
using Microsoft.Extensions.DependencyInjection;

namespace iiwi.Scheduler;

/// <summary>
/// Creates Quartz jobs using dependency injection container
/// </summary>
/// <remarks>
/// Implements <see cref="IJobFactory"/> to integrate Quartz with Microsoft's dependency injection system.
/// This factory resolves job instances from the service provider, enabling constructor injection
/// and proper lifecycle management for all scheduled jobs.
/// </remarks>
/// <param name="container">The service provider for dependency resolution</param>
public class MyJobFactory(IServiceProvider container) : IJobFactory
{
    /// <summary>
    /// Service provider instance for dependency resolution
    /// </summary>
    protected readonly IServiceProvider Container = container;

    /// <summary>
    /// Creates a new job instance using dependency injection
    /// </summary>
    /// <param name="bundle">Trigger and job details bundle</param>
    /// <param name="scheduler">Scheduler instance (not used in this implementation)</param>
    /// <returns>Resolved job instance from service container</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the job type cannot be resolved from the service provider
    /// </exception>
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler) =>
        (IJob)Container.GetRequiredService(bundle.JobDetail.JobType);

    /// <summary>
    /// Cleans up job resources after execution
    /// </summary>
    /// <param name="job">The job instance to dispose</param>
    /// <remarks>
    /// Disposes the job if it implements <see cref="IDisposable"/>.
    /// This method handles resource cleanup for jobs that require it.
    /// </remarks>
    public void ReturnJob(IJob job) => (job as IDisposable)?.Dispose();
}