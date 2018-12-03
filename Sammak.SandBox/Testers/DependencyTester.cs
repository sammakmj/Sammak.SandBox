using Sammak.SandBox.Common;
using Sammak.SandBox.DependencyManagement;
using Sammak.SandBox.Helpers;
using Sammak.SandBox.Services;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Sammak.SandBox.Testers
{
    public class DependencyTester
    {
        public static void Run()
        {
            new DependencyTester().FooBarTest();
        }

        private void FooBarTest()
        {
            //logger.LogInformation("Starting application");

            //do the actual work here
            var bar = AppData.ServiceProvider.GetService<IBarService>();
            if (bar is null)
            {
                ConsoleDisplay.ShowObject("IBarService is null", nameof(FooBarTest));
            }
            else
            {
                bar.DoSomeRealWork();
            }

            //var appSettingsService = AppData.ServiceProvider.GetService<IAppSettingsService>();
            //var env = appSettingsService.GetEnvironment();
            ////logger.LogInformation($"Env = {env}");

            ////logger.LogInformation("All done!");

            //ConsoleDisplay.ShowObject(env, nameof(appSettingsService.GetEnvironment));
        }

        private void PropertyTest()
        {
            var tst = new InterfaceImplementerPair();
            ConsoleDisplay.ShowObject(tst.Test, nameof(tst));
            //tst.Test = "sam";
            //ConsoleDisplay.ShowObject(tst.Test, nameof(tst));
        }

        private void RegisterTest()
        {
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\" + ConfigurationManager.AppSettings["DependenciesXml"];
            ConsoleDisplay.ShowObject(path, nameof(path));
            var imp = DependencyManagementService.InterfaceImplementersNames(path);
            foreach(var item in imp)
            {
                ConsoleDisplay.ShowObject(item.Value, item.Key);
                Console.WriteLine();
            }
        }

        private void AssemblyNameTest()
        {
            var assemblyPath = Assembly.GetExecutingAssembly().CodeBase;
            ConsoleDisplay.ShowObject(assemblyPath, nameof(assemblyPath));
            var uriBuilder = new UriBuilder(assemblyPath);
            ConsoleDisplay.ShowObject(uriBuilder, nameof(uriBuilder));
            var uriPath = uriBuilder.Path;
            ConsoleDisplay.ShowObject(uriPath, nameof(uriPath));
            var ss = Uri.UnescapeDataString(uriPath);
            ConsoleDisplay.ShowObject(ss, nameof(ss));
            var directoryName = Path.GetDirectoryName(ss);
            ConsoleDisplay.ShowObject(directoryName, nameof(directoryName));
            var dependenciesXml = ConfigurationManager.AppSettings["DependenciesXml"];
            ConsoleDisplay.ShowObject(dependenciesXml, nameof(dependenciesXml));
            var path = directoryName +"\\" + dependenciesXml;
            ConsoleDisplay.ShowObject(path, nameof(path));

        }
    }
}
