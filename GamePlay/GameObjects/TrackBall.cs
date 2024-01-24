using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.Utils;

namespace RythmGame.GameObjects
{
    public class TrackBall
    {
        private enum DirectionEnum
        {
            stop = 0,
            left = -1,
            right = 1
        };

        private bool running;
        private Texture2D texture;
        private DirectionEnum direction;
        private int speed;
        public Rectangle Rectangle;
        public int CenterX
        {
            get
            {
                return Rectangle.X + Rectangle.Width / 2;
            }
        }

        private double elapsedMovementTime;
        private double movementDelayTime;

        public TrackBall()
        {
            texture = AssetManager.GetTexture("trackBall");
            direction = DirectionEnum.stop;
            Rectangle.Width = 32;
            Rectangle.Height = 32;
            Rectangle.X = Globals.WindowWidth / 2 - Rectangle.Width / 2;
            Rectangle.Y = Globals.WindowHeight - Rectangle.Height - 50;
            speed = 3;
            movementDelayTime = 4;
        }

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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }

        public void Start()
        {
            running = true;
            direction = DirectionEnum.left;
        }

        public void Stop()
        {
            running = false;
            direction = DirectionEnum.stop;
        }

        public void Update(GameTime gameTime)
        {
            if(!running)
            {
                return;
            }

            elapsedMovementTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedMovementTime < movementDelayTime)
            {
                return;
            }

            Rectangle.X += (int)direction * speed;
            elapsedMovementTime = 0;
        }
    }
}
