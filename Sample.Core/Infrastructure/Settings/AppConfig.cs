using System;
using System.Configuration;
using System.Globalization;

namespace Sample.Core.Infrastructure.Settings
{
    public static class AppConfig
    {
        public static T GetSetting<T>(string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                throw new Exception(String.Format("Could not find setting '{0}',", name));
            }
            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        public static T TryGetSetting<T>(string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                return default(T);
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}