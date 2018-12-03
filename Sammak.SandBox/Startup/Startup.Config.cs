using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sammak.SandBox.Common;
using Sammak.SandBox.Models;
using System.IO;

namespace Sammak.SandBox
{
    /// <summary>
    /// Application setup Configuration
    /// </summary>
    public partial class Startup
    {
        public void ConfigureAppSettings()
        {
            var environmentName = "Development";
            var rootDirectory = Directory.GetCurrentDirectory();
            if(AppData.ServiceProvider != null)
            {
                var hostingEnvironment = AppData.ServiceProvider.GetService<IHostingEnvironment>();
                if(hostingEnvironment != null)
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

            AppData.Configuration = builder.Build();

            if (AppData.DependencyServices != null)
            {
                AppData.DependencyServices.Configure<AppSettings>(AppData.Configuration.GetSection("ApplicationSettings"));
            }
        }
    }
}
