using Microsoft.Extensions.Options;
using Sammak.SandBox.Models;

namespace Sammak.SandBox.Services.Impl
{
    public class AppSettingsService : IAppSettingsService
    {
        private readonly AppSettings _appSettings;

        public AppSettingsService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GetEnvironment()
        {
            var env = _appSettings.Environment;
            return env;
        }
    }
}
