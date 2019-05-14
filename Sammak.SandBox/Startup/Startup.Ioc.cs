using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sammak.Core.Common.Util;
using Sammak.Core.Common.Validation;
using Sammak.Core.Common.Validation.Impl;
using Sammak.SandBox.Common;
using Sammak.SandBox.Models.TokenString;
using Sammak.SandBox.Services;
using Sammak.SandBox.Services.Impl;
using StructureMap;

namespace Sammak.SandBox
{
    /// <summary>
    /// Structuremap IOC Container Setup - PersonSync APP
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Gets or builds the container
        /// </summary>
        /// <returns></returns>
        //public static IContainer EnsureIocSetUp()
        //{
        //    if (DependencyResolver.Container == null && !_initializing)
        //        return UseIoc();
        //    return DependencyResolver.Container;
        //}

        //private static bool _initializing;

        /// <summary>
        /// Gets or sets the container (private).
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        //public static IContainer Container { get; set; }

        //public void ConfigureIoc()
        //{
        //    AppData.DependencyServices = new ServiceCollection()
        //        .AddLogging()
        //        .AddSingleton<IFooService, FooService>()
        //        .AddSingleton<IBarService, BarService>()
        //        .AddSingleton<IAppSettingsService, AppSettingsService>()
        //        .AddSingleton<IValidationFactory, ValidationFactory>();

        //    AppData.ServiceProvider = AppData.DependencyServices.BuildServiceProvider();
        //}

        /// <summary>
        /// Builds the container (private).
        /// </summary>
        /// <returns></returns>
        //protected internal static IContainer UseIoc()
        //{
        //    //Logger.Trace("UseIoc: Initializing Container.");
        //    _initializing = true;

        //    try
        //    {
        //        Container = new Container(cfg =>
        //        {
        //            cfg.For<System.Configuration.Abstractions.IConfigurationManager>()
        //                .Use(() => DependencyResolver.ConfigurationManager);

        //            cfg.Scan(s =>
        //            {
        //                s.TheCallingAssembly();
        //                s.AssembliesFromApplicationBaseDirectory();
        //                s.IncludeNamespace("Sammak.SandBox.Ioc");
        //                s.LookForRegistries();
        //                s.WithDefaultConventions();
        //            });

        //            cfg.For<IValidationFactory>().Use<ValidationFactory>().Ctor<IContainer>("container").Is(DependencyResolver.Container);

        //            //c.For<ISessionFactory>().Singleton().Use(ConfigureOrm());
        //            //c.For<ISession>().LifecycleIs(new ThreadLocalStorageLifecycle()).Use(ctx => ctx.GetInstance<ISessionFactory>().OpenSession());
        //            //c.For<IBackgroundJobClient>().Use<BackgroundJobClient>();
        //            //c.For<IQueryFactory>().Use<QueryFactory>().Ctor<IContainer>("container").Is(DependencyResolver.Container);
        //            //c.For<Core.MassTransit.IBusFactory>().Use<Core.MassTransit.BusFactory>();
        //            //c.For<IEnumService>().Singleton().Use<EnumService>();
        //            //c.For<IValidator<AuditData>>().Singleton().Use<AuditData.AuditDataValidator>();
        //            //c.For<IAccessTokenService>().Singleton().Use<AccessTokenService>();

        //            FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<TokenStringModelValidator>()
        //                .ForEach(r =>
        //                {
        //                    cfg.For(r.InterfaceType)
        //                        .Singleton()
        //                        .Use(r.ValidatorType);
        //                });
        //        });
        //        DependencyResolver.Container = Container;
        //    }
        //    finally
        //    {
        //        _initializing = false;
        //    }

        //    //Logger.Trace("UseIoc: Container initialized.");
        //    return DependencyResolver.Container;
        //}
    }
}
