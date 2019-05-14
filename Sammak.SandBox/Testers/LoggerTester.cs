using Microsoft.Extensions.Logging;
using Sammak.SandBox.Helpers;

namespace Sammak.SandBox.Testers
{
    class LoggerTester
    {
        private readonly ILogger<LoggerTester> _logger;

        //public LoggerTester(ILogger<LoggerTester> logger)
        //{
        //    _logger = logger;
        //}

        public static void Run()
        {
            //var test = AppData.ServiceProvider.GetService<LoggerTester>();
            //test.AppSettingTest();
            new LoggerTester().Test();
        }

        private void Test()
        {
            //ILogger logger = null;
            //if (! (AppData.ServiceProvider is null))
            //{
            //    logger = AppData.ServiceProvider
            //        .GetService<ILoggerFactory>()
            //        .CreateLogger<MainTester>();
            //}

            //if (!(logger is null))
            //    logger.LogInformation("Starting application");

            var tester = "LoggerTester";
            ConsoleDisplay.ShowObject(tester, nameof(tester));

            //if (!(logger is null))
            //    logger.LogInformation("End application");
        }
    }
}
