using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Exceptions;
using Serilog.Extensions.Logging;
namespace DotnetAppWith.Hangfire.Example.Hangfire
{
    public static class ILoggerConfiguration
    {
        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var loggerConfiguration = new LoggerConfiguration()
            .Enrich.WithExceptionDetails();
            LogLevel logLevel;
            var options = configuration.GetSection("LoggingOptions").Get<LoggingOptions>();

            if (options!.UseFile)
            {
                loggerConfiguration.WriteTo.Async(q => q.File("Logs/log.txt", rollOnFileSizeLimit: true, shared: true, rollingInterval: RollingInterval.Day));
            }
            switch (options.Level.ToUpperInvariant())
            {
                case "DEBUG":
                    logLevel = LogLevel.Debug;
                    loggerConfiguration.MinimumLevel.Debug();
                    break;
                case "WARNING":
                    logLevel = LogLevel.Warning;
                    loggerConfiguration.MinimumLevel.Warning();
                    break;
                case "INFORMATION":
                    logLevel = LogLevel.Information;
                    loggerConfiguration.MinimumLevel.Information();
                    break;
                default:
                    logLevel = LogLevel.Error;
                    loggerConfiguration.MinimumLevel.Error();
                    break;
            }

            Log.Logger = loggerConfiguration.CreateLogger();
            services.AddSingleton<ILoggerProvider>(new SerilogLoggerProvider(Log.Logger));
            services.TryAddSingleton<ILoggerFactory>(q => new LoggerFactory(q.GetServices<ILoggerProvider>(), new LoggerFilterOptions { MinLevel = logLevel }));
            services.TryAddSingleton(typeof(ILogger<>), typeof(Logger<>));
            return services;
        }
    }

    public class LoggingOptions
    {
        public string Level { get; set; } = string.Empty;
        public bool UseApplicationInsights { get; set; } = false;

        public string InstrumetationKey { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public bool UseTableStorage { get; set; } = false;
        public bool UseFile { get; set; } = false;
    }
}
