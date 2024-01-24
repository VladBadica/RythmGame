using System;
using System.IO;

namespace RythmGame.Utils
{
    public static class MapLoader
    {

        public static string[] GetAllMaps()
        {
            return Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\maps");            
        }
    }
}
