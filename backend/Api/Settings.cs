using System.Configuration;

namespace Api
{
    class Settings
    {
        public static string Url { get; private set; }
        public static string Port { get; private set; }

        public static string AppName { get; private set; }



        public static string ShortUrlPart { get; private set; }

        static Settings()
        {
            Url = ConfigurationManager.AppSettings["url"];
        Port = ConfigurationManager.AppSettings["port"];
        AppName = ConfigurationManager.AppSettings["appName"];
            ShortUrlPart = ConfigurationManager.AppSettings["shortUrlPart"];
    }
    }
}
