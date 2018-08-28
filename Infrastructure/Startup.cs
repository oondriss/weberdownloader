using System.Diagnostics;
using System.IO;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestApp.DbModels;
using TestApp.Extensions;
using TestApp.HangFire;
using TestApp.Services;

namespace TestApp.Infrastructure
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            
			Configuration = configuration;
		}

	    // ReSharper disable once MemberCanBePrivate.Global
	    public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddDbContext<DatabaseContext>(i =>
                {
                    i.UseSqlite(Configuration.GetValue<string>("SqliteFileName"));
                });
                
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddHangfire(x => x.UseMemoryStorage());
                services.AddTransient<IDbManager, DbManager>();
                services.AddTransient<IJobManager, JobManager>();
                services.AddTransient<IWeberReader, WeberReader>();
                services.AddTransient<ICronValitador, CronValidator>();
                services.AddTransient<IIpValidator, IpValidator>();
                services.AddMvc().AddRazorPagesOptions(o =>
                {
                    o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
                });
            }
            catch (System.Exception ex)
            {
                File.WriteAllText(Program.LogFile, ex.GetLogMessage());
                throw;
            }
        }
		
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DatabaseContext dabataseContext, IJobManager jobManager)
        {
            try
            {
                loggerFactory.AddLog4Net();

                if (env.IsDevelopment())
                    app.UseDeveloperExceptionPage();
                else
                    app.UseExceptionHandler("/Home/Error");

                app.UseStaticFiles();

                GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(app.ApplicationServices));
                app.UseHangfireServer();
                app.UseHangfireDashboard("/jobs");

                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        "default",
                        "{controller=Home}/{action=Index}/{id?}");
                });
                dabataseContext.Database.Migrate();
                jobManager.CreateAllHeadsJobs();
            }
            catch (System.Exception ex)
            {
                File.WriteAllText(Program.LogFile, ex.GetLogMessage());
                throw;
            }
        }
    }
}
