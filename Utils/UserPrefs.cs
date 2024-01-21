using Newtonsoft.Json;
using System;
using System.IO;

namespace RythmGame.Utils
{
    public static class UserPrefs
    {
        public static UserPrefsObject Settings;

        public static void Initialize()
        {
            string fileName = "userPrefs.json";
            string path = $"{Directory.GetCurrentDirectory()}\\{fileName}";
            if (File.Exists(path))
            {
                Settings = JsonConvert.DeserializeObject<UserPrefsObject>(File.ReadAllText(path));
            }
            else
            {
                throw new Exception("User Prefs file not found");
            }
        }
    }
}
