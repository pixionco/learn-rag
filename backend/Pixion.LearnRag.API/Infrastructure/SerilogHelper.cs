using Serilog;

namespace Pixion.LearnRag.API.Infrastructure
{
    public static class SerilogHelper
    {
        //helper method for getting the IConfiguration for Serilog before we build it for real
        public static IConfiguration BuildConfigurationForSerilog() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("serilog.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"serilog.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

        public static LoggerConfiguration BuildSerilogLoggerConfiguration() =>
            new LoggerConfiguration()
                .ReadFrom
                .Configuration(BuildConfigurationForSerilog());
    }
}
