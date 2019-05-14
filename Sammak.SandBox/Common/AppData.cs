using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sammak.Core.Common.Validation;
using Sammak.Core.Common.Validation.Impl;
using Sammak.SandBox.Models;
using Sammak.SandBox.Providers;
using Sammak.SandBox.Services;
using Sammak.SandBox.Services.Impl;
using Sammak.SandBox.Testers;
using Serilog;
using System;
using System.IO;

namespace Sammak.SandBox.Common
{
    public class AppData
    {
        public static IServiceCollection ServiceCollection;
        public static IServiceProvider ServiceProvider;
        public static IConfiguration Configuration;
        public static IConfigurationRoot configuration;

        public static void ConfigureIoc()
        {
            ServiceCollection = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IFooService, FooService>()
                .AddSingleton<IBarService, BarService>()
                .AddSingleton<IAppSettingsService, AppSettingsService>()
                .AddSingleton<IValidationFactory, ValidationFactory>();

            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        public static void ConfigureAppSettings()
        {
            var environmentName = "Development";
            var rootDirectory = Directory.GetCurrentDirectory();
            if (ServiceProvider != null)
            {
                var hostingEnvironment = ServiceProvider.GetService<IHostingEnvironment>();
                if (hostingEnvironment != null)
                {
                    environmentName = hostingEnvironment.EnvironmentName;
                    rootDirectory = hostingEnvironment.ContentRootPath;
                }
            }
            var builder = new ConfigurationBuilder()
                .SetBasePath(rootDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            if (ServiceCollection != null)
            {
                ServiceCollection.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));
            }
        }

        public static void ConfigureLogging()
        {
            if (ServiceCollection != null)
            {
                ServiceCollection
                    .AddLogging();

                if (ServiceProvider != null)
                {
                    var loggerFactory = ServiceProvider
                        .GetService<ILoggerFactory>();

                    loggerFactory.AddProvider(new CustomLoggerProvider());

                    //loggerFactory.AddConsole(LogLevel.Debug);
                }
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Add logging
            //serviceCollection.AddSingleton(new LoggerFactory()
            //    //.AddConsole()
            //    .AddSerilog()
            //    //.AddDebug()
            //    );
            serviceCollection.AddLogging();

            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Initialize serilog logger
            Log.Logger = new LoggerConfiguration()
                 .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
                 .MinimumLevel.Debug()
                 .Enrich.FromLogContext()
                 .CreateLogger();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);

            // Add services
            //serviceCollection.AddTransient<IBackupService, BackupService>();
            //serviceCollection.AddTransient<IRestoreService, RestoreService>();

            // Add app
            serviceCollection.AddTransient<FunctionTest>();
        }
    }
}
