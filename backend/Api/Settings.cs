using System.Configuration;

namespace Api
{
    class Settings
    {
        public static string Url => ConfigurationManager.AppSettings["url"];
        public static string Port => ConfigurationManager.AppSettings["port"];

        public static string AppName => ConfigurationManager.AppSettings["appName"];
    }
}
