using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sammak.SandBox.Common
{
    public class AppData
    {
        internal static IServiceCollection DependencyServices;

        internal static ServiceProvider ServiceProvider;

        internal static IConfiguration Configuration;
    }
}
