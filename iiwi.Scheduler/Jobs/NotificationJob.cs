
using Quartz;

namespace iiwi.Scheduler.Jobs;

/// <summary>Send Notifications</summary>
public class NotificationJob : IJob
{
    /// <summary>Called by the <see cref="T:Quartz.IScheduler" /> when a <see cref="T:Quartz.ITrigger" />
    /// fires that is associated with the <see cref="T:Quartz.IJob" />.</summary>
    /// <param name="context">The execution context.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    /// <remarks>
    /// The implementation may wish to set a  result object on the
    /// JobExecutionContext before this method exits.  The result itself
    /// is meaningless to Quartz, but may be informative to
    /// <see cref="T:Quartz.IJobListener" />s or
    /// <see cref="T:Quartz.ITriggerListener" />s that are watching the job's
    /// execution.
    /// </remarks>
    public Task Execute(IJobExecutionContext context)
    {
        // Code that sends a periodic email to the user (for example)
        // Note: This method must always return a value 
        // This is especially important for trigger listers watching job execution
        Console.WriteLine("Hello from Notification");
        return Task.FromResult(true);
    }
}
