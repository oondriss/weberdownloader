using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TestApp.DbModels;
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
			services.AddDbContext<DatabaseContext>(i =>
			{
				i.UseSqlite(Configuration.GetValue<string>("SqliteFileName"));
			});
			
			services.AddElm(opts =>
			{
				opts.Path = new PathString("/logs");
				opts.Filter = (name, level) => level >= LogLevel.Trace;
			});
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddHangfire(x => x.UseMemoryStorage());
			services.AddTransient<IDbManager, DbManager>();
	        services.AddTransient<IJobManager, JobManager>();
			services.AddTransient<IWeberReader, WeberReader>();
	        services.AddTransient<ICronValitador, CronValidator>();
			services.AddMvc();
        }
		
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DatabaseContext dabataseContext)
        {
			loggerFactory.AddLog4Net();
			
			app.UseElmPage();
			app.UseElmCapture();

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
		}
    }
}
