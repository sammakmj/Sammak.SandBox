using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sammak.SandBox.Common;

namespace Sammak.SandBox.Testers
{
    class MainTester
    {
        public static void Run()
        {
            ILogger logger = null;
            if (! (AppData.ServiceProvider is null))
            {
                logger = AppData.ServiceProvider
                    .GetService<ILoggerFactory>()
                    .CreateLogger<MainTester>();
            }

            if (!(logger is null))
                logger.LogInformation("Starting application");

            FunctionTest.Run();

            if (!(logger is null))
                logger.LogInformation("End application");
        }
    }
}
