using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.IO;

namespace RythmGame.Utils
{
    public static class UserPrefs
    {
        public static UserPrefsObject Settings;

        public static UserPrefsObject GetDefaultSettings() {
            return new UserPrefsObject()
            {
                LeftActionKey = Keys.Z,
                RightActionKey = Keys.X,
                MasterVolume = 0.05f,
                MusicVolume = 1,
                EffectsVolume = 1
            };
        }


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
                Settings = GetDefaultSettings();
                File.WriteAllText(path, JsonConvert.SerializeObject(GetDefaultSettings()));
            }
        }
    }
}
