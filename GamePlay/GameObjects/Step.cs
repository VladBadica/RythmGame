using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.Utils;

namespace RythmGame.GameObjects
{
    public class Step : GameObject
    {
        private Texture2D texture;
        private Color color = Color.White;

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, color);
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
