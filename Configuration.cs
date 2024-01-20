using System.Configuration;

namespace RythmGame
{
    public class Configuration
    {
        public static int ScreenWidth => int.Parse(ConfigurationManager.AppSettings["ScreenWidth"]);
        
        public static int ScreenHeight => int.Parse(ConfigurationManager.AppSettings["ScreenHeight"]);

    }
}
