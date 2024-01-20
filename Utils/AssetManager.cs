using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RythmGame.Utils
{
    public static class AssetManager
    {
        private static ContentManager content;

        public static void Initialize(ContentManager Content)
        {
            content = Content;
        }

        public static Texture2D GetTexture(string name)
        {
            return content.Load<Texture2D>(name);
        }
    }
}
