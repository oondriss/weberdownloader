namespace TestApp;

// ReSharper disable once ClassNeverInstantiated.Global
public class Program
{
    private static string _appDirectory;
    public static string LogFile;

    public static void Main(string[] args)
    {

        string pathToExe = Process.GetCurrentProcess().MainModule.FileName;
        _appDirectory = Path.GetDirectoryName(pathToExe);
        LogFile = Path.Combine(_appDirectory, "startuplog.txt");

        bool isRunningDirectly = Debugger.IsAttached || args.Contains("console");

        if (!isRunningDirectly)
        {
            Directory.SetCurrentDirectory(_appDirectory);
        }

        try
        {
            IWebHost host = WebHost.CreateDefaultBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureServices((host, services) =>
                {
                    _ = services.AddDbContext<DatabaseContext>(i => i.UseSqlite(host.Configuration.GetValue<string>("SqliteFileName")));
                    _ = services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                    _ = services.AddHangfire(x => x.UseMemoryStorage());
                    _ = services.AddHangfireServer();
                    _ = services.AddTransient<Services.DbManager.IDbManager, Services.DbManager.DbManager>();
                    _ = services.AddTransient<IJobManager, JobManager>();
                    _ = services.AddTransient<IWeberReader, WeberReader>();
                    _ = services.AddTransient<ICronValitador, CronValidator>();
                    _ = services.AddTransient<IIpValidator, IpValidator>();
                    _ = services.AddMvc();
                })
                .ConfigureLogging(app => app.AddLog4Net())
                .Configure((host, app) =>
                {
                    if (host.HostingEnvironment.IsDevelopment())
                    {
                        _ = app.UseDeveloperExceptionPage();
                    }
                    else
                    {
                        _ = app.UseExceptionHandler("/Home/Error");
                    }

                    _ = app.UseStaticFiles();
                    _ = app.UseRouting();
                    app.SetupHangfire(host);
                    _ = app.UseEndpoints(i => i.MapDefaultControllerRoute());

                    using IServiceScope scope = app.ApplicationServices.CreateScope();
                    DatabaseContext dabataseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    IJobManager jobManager = scope.ServiceProvider.GetRequiredService<IJobManager>();
                    dabataseContext.Database.Migrate();
                    _ = jobManager.CreateAllHeadsJobs();
                })
                .UseUrls("http://localhost:5000")
                .Build();

            if (Debugger.IsAttached || args.Contains("console"))
            {
                host.Run();
            }
            else
            {
                host.RunAsCustomService();
            }
        }
        catch (System.Exception ex)
        {
            File.WriteAllText(LogFile, ex.GetLogMessage());
            throw;
        }
    }
}
