using Microsoft.Extensions.Configuration;
using Sammak.SandBox.Helpers;
using System;
using System.IO;

namespace Sammak.SandBox.Testers
{
    public class AppSettingsTester
    {
         public static IConfigurationRoot ConfigurationRoot;

        public static void Run()
        {
            new AppSettingsTester().AppSettingTest();
        }

        private void AppSettingTest()
        {
        ConfigurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var ApplicationName = ConfigurationRoot["ApplicationSettings:ApplicationName"];
            ConsoleDisplay.ShowObject(ApplicationName, nameof(ApplicationName));
        }

        private void EnvironmentValueTest()
        {
            throw new NotImplementedException();
        }

        //private string GetEnvironment(IOptions<AppSettingsService> service)
        //{
        //    return service.g
        //}
    }
}
