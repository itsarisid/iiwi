using Quartz.Impl;
using Quartz.Spi;
using Quartz;

namespace iiwi.Scheduler;

/// <summary>
/// Configures and manages Quartz scheduler with dependency injection support
/// </summary>
/// <remarks>
/// Singleton class that provides centralized configuration for Quartz scheduling.
/// Integrates with dependency injection through custom job factory and offers
/// simplified methods for job registration and scheduler management.
/// </remarks>
public class MyQuartz
{
    // The scheduler used
    private IScheduler _scheduler = null!;

    /// <summary>
    /// Gets the configured Quartz scheduler instance
    /// </summary>
    public static IScheduler Scheduler => Instance._scheduler;

    // Singleton instance
    private static MyQuartz _instance = null!;

    /// <summary>
    /// Gets the singleton instance of MyQuartz
    /// </summary>
    public static MyQuartz Instance
    {
        get
        {
            _instance ??= new MyQuartz();
            return _instance;
        }
    }

    /// <summary>
    /// Private constructor for singleton pattern
    /// </summary>
    private MyQuartz()
    {
        // Initialize
        _ = Init();
    }

    /// <summary>
    /// Initializes the scheduler instance
    /// </summary>
    private async Task Init()
    {
        // Set scheduler with standard Scheduler Factory
        _scheduler = await new StdSchedulerFactory().GetScheduler();
    }

    /// <summary>
    /// Configures the job factory for dependency injection
    /// </summary>
    /// <param name="jobFactory">The job factory to use for creating job instances</param>
    /// <returns>The configured scheduler instance</returns>
    /// <remarks>
    /// Use this method to set up dependency injection support for job creation
    /// </remarks>
    public IScheduler UseJobFactory(IJobFactory jobFactory)
    {
        Scheduler.JobFactory = jobFactory;
        return Scheduler;
    }

    /// <summary>
    /// Adds a new job to the scheduler with specified interval
    /// </summary>
    /// <typeparam name="T">Job type implementing IJob</typeparam>
    /// <param name="name">Unique name for the job</param>
    /// <param name="group">Group name for job organization</param>
    /// <param name="interval">Execution interval in seconds</param>
    /// <remarks>
    /// Creates a simple repeating job that starts immediately and runs forever
    /// </remarks>
    public static async Task AddJob<T>(string name, string group, int interval)
        where T : IJob
    {
        // Create Job
        IJobDetail job = JobBuilder.Create<T>()
            .WithIdentity(name, group)
            .Build();

        // Create Trigger
        ITrigger jobTrigger = TriggerBuilder.Create()
            .WithIdentity(name + "Trigger", group)
            .StartNow() // Start now
            .WithSimpleSchedule(t => t.WithIntervalInSeconds(interval).RepeatForever()) // With repetition every interval seconds
            .Build();

        // Add Job
        await Scheduler.ScheduleJob(job, jobTrigger);
    }

    /// <summary>
    /// Starts the Quartz scheduler
    /// </summary>
    /// <remarks>
    /// Begins execution of all scheduled jobs. Call this after configuring all jobs.
    /// </remarks>
    public static async Task Start()
    {
        await Scheduler.Start();
    }

    /// <summary>
    /// Stops the Quartz scheduler gracefully
    /// </summary>
    /// <remarks>
    /// Allows currently executing jobs to complete before shutting down
    /// </remarks>
    public static async Task Stop()
    {
        await Scheduler.Shutdown(true);
    }
}