using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.Utils;

namespace RythmGame.GameObjects
{
    public class Step : GameObject
    {
        private Texture2D texture;
        private Color color = Color.White;

        public Rectangle Position;

        public Step()
        {
            texture = AssetManager.GetTexture("stepLine");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, color);
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
