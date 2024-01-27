using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.Utils;

namespace RythmGame.GamePlay.Track
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
        private float speed;
        private Vector2 position;
        private Vector2 size;
        public int CenterX
        {
            get
            {
                return (int)position.X + (int)size.X / 2;
            }
        }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            }
        }

        public TrackBall(float speed = 1f)
        {
            texture = AssetManager.GetTexture("trackBall");
            direction = DirectionEnum.left;
            size.X = 32;
            size.Y = 32;
            position.X = Globals.WindowWidth / 2 - size.X / 2;
            position.Y = Globals.WindowHeight - size.Y - 50;
            this.speed = speed;
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

        public void Reset()
        {
            position.X = Globals.WindowWidth / 2 - size.X / 2;
            position.Y = Globals.WindowHeight - size.Y - 50;
            direction = DirectionEnum.left;
        }

        public void Start()
        {
            running = true;
        }

        public void Stop()
        {
            running = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!running)
            {
                return;
            }

            position.X += (int)direction * speed;
        }
    }
}
