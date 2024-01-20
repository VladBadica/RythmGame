using System.Configuration;

namespace RythmGame
{
    public class Configuration
    {
        public static int WindowWidth => int.Parse(ConfigurationManager.AppSettings["WindowWidth"]);
        
        public static int WindowHeight => int.Parse(ConfigurationManager.AppSettings["WindowHeight"]);

    }
}
