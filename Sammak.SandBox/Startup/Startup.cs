using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sammak.SandBox.Common;
using Sammak.SandBox.Models;
using Sammak.SandBox.Testers;
using System.IO;

namespace Sammak.SandBox
{
    /// <summary>
    /// </summary>
    public partial class Startup
    {
        public IServiceCollection Services { get; set; }

        //public Startup()
        //{
        //}

        public static void ConfigureAndRun()
        {
            //UseIoc();
            //var startup = new Startup();
            AppData.ConfigureIoc();
            AppData.ConfigureAppSettings();
            AppData.ConfigureLogging();

            MainTester.Run();
        }

        //// This method gets called by the runtime. Use this method to add services to the container.
        //// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    var configSection = Configuration.GetSection("ApplicationSettings");
        //    //services.Configure<AppSettings>(configSection);
        //    //services.Configure<AppSettings>(  // (Configuration.GetSection("ApplicationSettings"));
        //}

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //}
    }
}
