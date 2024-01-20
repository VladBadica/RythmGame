using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.Utils;
using System;

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
        private int speed = 2;

        private double elapsedMovementTime;
        private double MovementDelayTime = 5;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, color);
        }

        public override void Initialize()
        {
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

            if (Rectangle.X + Rectangle.Width > Configuration.WindowWidth / 2 + Configuration.WindowWidth / 4)
            {
                direction = DirectionEnum.left;
            }
            if (Rectangle.X + Rectangle.Width < Configuration.WindowWidth / 2 - Configuration.WindowWidth / 4)
            {
                direction = DirectionEnum.right;
            }

            Rectangle.X += (int)direction * speed;
            elapsedMovementTime = 0;
        }
    }
}
