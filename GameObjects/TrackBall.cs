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
        private Rectangle position;
        private Color color = Color.White;
        private DirectionEnum direction = DirectionEnum.left;
        private int speed = 2;

        private double elapsedMovementTime;
        private double MovementDelayTime = 5;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }

        public override void Initialize()
        {
            position.Width = 32;
            position.Height = 32;
            position.X = Configuration.WindowWidth / 2 - position.Width / 2;
            position.Y = Configuration.WindowHeight - position.Height - 50;
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

            if (position.X + position.Width > Configuration.WindowWidth / 2 + Configuration.WindowWidth / 4)
            {
                direction = DirectionEnum.left;
            }
            if (position.X + position.Width < Configuration.WindowWidth / 2 - Configuration.WindowWidth / 4)
            {
                direction = DirectionEnum.right;
            }

            position.X += (int)direction * speed;
            elapsedMovementTime = 0;
        }
    }
}
