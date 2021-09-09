using FluentMigrator.Runner;
using manager.Jobs;
using manager.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace manager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private const string connectionString = @"Data Source=metricsManager.db; Version=3;";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<CpuMetricsRepository>();
            services.AddSingleton<AgentRepository>();
            services.AddControllersWithViews();
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                .AddFluentMigratorConsole());
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddHttpClient();
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton<RamMetricJob>();
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton(new JobSchedule(jobType: typeof(CpuMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(RamMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(DotNetMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(HddMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(NetworkMetricJob), cronExpression: "0/5 * * * * ?"));

            services.AddHostedService<QuartzHostedService>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            migrationRunner.MigrateUp();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API менеждера сбора метрик");
                c.RoutePrefix = string.Empty;
            });
        }

    }
}
