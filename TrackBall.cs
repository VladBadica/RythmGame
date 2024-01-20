using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RythmGame
{
    public class TrackBall
    {
        private Texture2D texture;
        private Vector2 position;
        private Color color = Color.White;

        private enum directionEnum {
            left = -1,
            right = 1
        };
        private directionEnum direction = directionEnum.left;
        private int speed = 2;

        public TrackBall(Texture2D texture)
        {
            this.texture = texture;
            position.X = Configuration.WindowWidth / 2 - texture.Width / 2;
            position.Y = Configuration.WindowHeight - texture.Height - 50;
        }

        public void Update()
        {
            if (position.X > Configuration.WindowWidth / 2 + Configuration.WindowWidth / 4)
            {
                direction = directionEnum.left;
            }
            if (position.X < Configuration.WindowWidth / 2 - Configuration.WindowWidth / 4)
            {
                direction = directionEnum.right;
            }

            position.X += (int)direction * speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }
}
