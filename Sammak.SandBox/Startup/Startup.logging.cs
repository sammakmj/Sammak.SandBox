using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sammak.SandBox.Common;
using Sammak.SandBox.Providers;

namespace Sammak.SandBox
{
    /// <summary>
    /// Logging configuration
    /// </summary>
    public partial class Startup
    {
        public void ConfigureLogging()
        {
            if (AppData.DependencyServices != null)
            {
                AppData.DependencyServices
                    .AddLogging();

                if (AppData.ServiceProvider != null)
                {
                    var loggerFactory = AppData.ServiceProvider
                        .GetService<ILoggerFactory>();

                    loggerFactory.AddProvider(new CustomLoggerProvider());

                    //loggerFactory.AddConsole(LogLevel.Debug);
                }
            }
        }
    }
}
