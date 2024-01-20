using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

        public static SpriteFont GetSpriteFont(string name)
        {
            return content.Load<SpriteFont>(name);
        }

        public static SoundEffect GetSoundEffect(string name)
        {
            return content.Load<SoundEffect>(name);
        }
    }
}
