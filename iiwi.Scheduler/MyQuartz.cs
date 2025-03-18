using Quartz.Impl;
using Quartz.Spi;
using Quartz;

namespace iiwi.Scheduler;

/// <summary>
/// Configuration for den Quartz Scheduler
/// </summary>
public class MyQuartz
{
    // Der verwendete Scheduler
    private IScheduler _scheduler;

    /// <summary>
    /// Verwendete Scheduler
    /// </summary>
    public static IScheduler Scheduler { get { return Instance._scheduler; } }

    // Singleton
    private static MyQuartz _instance = null;

    /// <summary>
    /// Singleton
    /// </summary>
    public static MyQuartz Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MyQuartz();
            }
            return _instance;
        }
    }

    private MyQuartz()
    {
        // Initialisieren
        Init();
    }

    private async void Init()
    {
        // Scheduler setzen mit standard Scheduler Factory
        _scheduler = await new StdSchedulerFactory().GetScheduler();
    }

    /// <summary>
    /// Legt die JobFactory fest aus der die jobs erzeugt werden
    /// </summary>
    /// <param name="jobFactory"></param>
    /// <returns></returns>
    public IScheduler UseJobFactory(IJobFactory jobFactory)
    {
        Scheduler.JobFactory = jobFactory;
        return Scheduler;
    }

    /// <summary>
    /// Fügt einen neuen Job dem Scheduler hinzu
    /// </summary>
    /// <typeparam name="T">Job der erzeugt wird</typeparam>
    /// <param name="name">Name des Jobs</param>
    /// <param name="group">Gruppe des jobs</param>
    /// <param name="interval">Interval zweischen Ausführung in sekunden</param>
    public async void AddJob<T>(string name, string group, int interval)
        where T : IJob
    {
        // Job erstellen
        IJobDetail job = JobBuilder.Create<T>()
            .WithIdentity(name, group)
            .Build();

        // Trigger erstellen
        ITrigger jobTrigger = TriggerBuilder.Create()
            .WithIdentity(name + "Trigger", group)
            .StartNow() // Jetzt starten
            .WithSimpleSchedule(t => t.WithIntervalInSeconds(interval).RepeatForever()) // Mit wiederholung alle interval sekunden
            .Build();

        // Job anfügen
        await Scheduler.ScheduleJob(job, jobTrigger);
    }

    /// <summary>
    /// Startet den Scheduler
    /// </summary>
    public static async void Start()
    {
        await Scheduler.Start();
    }
}
