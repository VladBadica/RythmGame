using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.Utils;

namespace RythmGame.GameObjects
{
    public class TrackBall : GameObject
    {
        private enum DirectionEnum
        {
            stop = 0,
            left = -1,
            right = 1
        };

        private Texture2D texture;
        public Rectangle Rectangle;
        private Color color = Color.White;
        private DirectionEnum direction = DirectionEnum.left;
        private int speed = 4;

        private double elapsedMovementTime;
        private double MovementDelayTime = 5;

        public void ChangeDirection()
        {
            if (direction == DirectionEnum.left)
            {
                direction = DirectionEnum.right; 
            }
            else if (direction == DirectionEnum.right)
            {
                direction = DirectionEnum.left;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, color);
        }

        public override void Initialize()
        {
            direction = DirectionEnum.left;
            Rectangle.Width = 32;
            Rectangle.Height = 32;
            Rectangle.X = Configuration.WindowWidth / 2 - Rectangle.Width / 2;
            Rectangle.Y = Configuration.WindowHeight - Rectangle.Height - 50;
        }

        public override void LoadContent()
        {
            texture = AssetManager.GetTexture("trackBall");
        }

        public override void Update(GameTime gameTime)
        {
            elapsedMovementTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedMovementTime < MovementDelayTime)
            {
                return;
            }

            Rectangle.X += (int)direction * speed;
            elapsedMovementTime = 0;
        }
    }
}
