using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RythmGame
{
    public class Marker
    {
        private Texture2D texture;
        private Vector2 position;
        private Color color = Color.White;

        public Marker(Texture2D texture)
        {
            this.texture = texture;
            position.X = 0;
            position.Y = 0;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }
}
