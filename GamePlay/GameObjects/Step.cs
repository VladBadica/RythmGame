using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.Utils;

namespace RythmGame.GameObjects
{
    public class Step
    {
        private Texture2D texture;

        public Rectangle Rectangle;
        public int CenterX
        {
            get
            {
                return Rectangle.X + Rectangle.Width / 2;
            }
        }

        public Step()
        {
            texture = AssetManager.GetTexture("stepLine");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
