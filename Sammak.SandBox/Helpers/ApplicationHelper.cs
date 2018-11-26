using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace Sammak.SandBox.Helpers
{
    public static class ApplicationHelper
    {
        //UserManagement UserProfileService
        private static string _applicationName = string.Empty;
        public static string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationName))
                {
                    _applicationName = GetAppSettingValue("ApplicationName");
                }
                return _applicationName;
            }
        }

        /// <summary>
        /// Returns value for an application setting defined in web.config
        /// </summary>
        /// <param name="key">key of an appSetting entry of the web.config</param>
        /// <returns>>value of an appSetting entry of the web.config</returns>
        public static string GetAppSettingValue(string key)
        {
            string appSettingValue = ConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(appSettingValue) ? string.Empty : appSettingValue;
        }

        /// <summary>
        /// Gets specified Configuration Section from applicationSettings section defined in web.config
        /// </summary>
        /// <param name="sectionName">Name of the Configuration Section to be retrieved</param>
        /// <returns>ConfigurationSection</returns>
        public static ConfigurationSection GetApplicationSettingsSection(string sectionName)
        {
            ConfigurationSection retVal = ((ConfigurationSection)ConfigurationManager.GetSection(string.Format("applicationSettings/{0}", sectionName)));
            return retVal;
        }

        public static void CreateDirectory(this DirectoryInfo dirInfo)
        {
            if (dirInfo.Parent != null) CreateDirectory(dirInfo.Parent);
            if (!dirInfo.Exists) dirInfo.Create();
        }

        public static bool IsRegexPatternValid(string pattern)
        {
            try
            {
                new Regex(pattern);
                return true;
            }
            catch { }
            return false;
        }
    }
}
