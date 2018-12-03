using Microsoft.Extensions.Options;
using Sammak.SandBox.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sammak.SandBox.Testers
{
    public class AppSettingsTester
    {
        public static void Run()
        {
            new AppSettingsTester().EnvironmentValueTest();
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
