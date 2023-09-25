using DotnetAppWith.Hangfire.Example.Hangfire.Jobs;
using DotnetAppWith.Hangfire.Example.Services;
using Hangfire;

namespace DotnetAppWith.Hangfire.Example.Hangfire
{
    public static class HanfireScheduleConfiguration
    {
        public static IServiceCollection AddHangfireScheduleJob(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(configuration.GetConnectionString("sqlConnection"));
            });
            services.AddHangfireServer();
            return services;
        }

        public static IApplicationBuilder AddOrUpdateHangFireScheduleJob(this IApplicationBuilder app, WebApplication application, IConfiguration configuration)
        {
            //old
            //RecurringJob.AddOrUpdate<AutoAddActor>(x => x.ExecuteJob(), configuration["AutoAddActorCron"], TimeZoneInfo.Utc);
            //RecurringJob.AddOrUpdate("AutoAddActor", () => { var result = new AutoAddActor().ExecuteJob(); }, "0 12 * */2");
            using (var serviceScope = application.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var actorService = services.GetRequiredService<IActorService>();
                var autoAddActorLogger = services.GetRequiredService<ILogger<AutoAddActor>>();

                RecurringJob.AddOrUpdate("AutoAddActor", () => new AutoAddActor(actorService, autoAddActorLogger).ExecuteJob(), Cron.Minutely);
            }

            //RecurringJob.AddOrUpdate("some-id", () => , "*/15 * * * *");
            return app;
        }
    }
}
