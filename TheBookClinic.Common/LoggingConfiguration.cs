using System.Configuration;
using Serilog;
using Serilog.Core;
using TheBookClinic.Common.Logging;

namespace TheBookClinic.Common
{
    public static class LoggingConfiguration
    {
        public static string Path { get; } = ConfigurationManager.AppSettings["log_path"];

        public static string SeqUrl { get; } = ConfigurationManager.AppSettings["seq_url"];

        public static Logger ConfigureLogger(string serviceName)
        {
            var configuration = new LoggerConfiguration()
                .Enrich.With(new ServiceNameEnricher(serviceName))
                .WriteTo.Console()
                .WriteTo.RollingFile($"{Path}{serviceName}" + "-{Date}.log")
                .WriteTo.Seq(SeqUrl)
                .CreateLogger();
            Log.Logger = configuration;

            return configuration;
        }
    }
}