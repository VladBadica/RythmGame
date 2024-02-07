using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.Utils;
using System;

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

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            }
        }

        public event EventHandler<int> OnCorrectHit;
        public event EventHandler OnMiss;
        public event EventHandler OnOutOfBounds;
        public int ChangeDirectionAt;
        public bool CurrentStepHit;

        public TrackBall(float speed = 1f)
        {
            texture = AssetManager.GetTexture("trackBall2");
            direction = DirectionEnum.left;
            size.X = 48;
            size.Y = 48;
            position.X = Globals.WindowWidth / 2 - size.X / 2;
            position.Y = Globals.WindowHeight - size.Y - 50;
            this.speed = speed;
            CurrentStepHit = false;
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

        public void Draw()
        {
            Globals.SpriteBatch.Draw(texture, Rectangle, Color.White);
        }

        public void GetNewChangeDirectionAt(Rectangle currentStep)
        {
            ChangeDirectionAt = direction == DirectionEnum.left ? currentStep.X - 1 : currentStep.X + currentStep.Width + 1;
        }

        public void Reset()
        {
            position.X = Globals.WindowWidth / 2 - size.X / 2;
            position.Y = Globals.WindowHeight - size.Y - 50;
            direction = DirectionEnum.left;
            CurrentStepHit = false;
            ChangeDirectionAt = 0;
        }

        public void Start()
        {
            running = true;
        }

        public void Stop()
        {
            running = false;
        }

        public void Update(TrackEngine trackEngine)
        {
            if (!running)
            {
                return;
            }

            position.X += (int)direction * speed;

            if (InputHandler.IsKeyPressed(UserPrefs.Settings.LeftActionKey) || InputHandler.IsKeyPressed(UserPrefs.Settings.RightActionKey))
            {
                if (Rectangle.Intersects(trackEngine.Map.CurrentStep))
                {
                    OnCorrectHit?.Invoke(this, trackEngine.Map.CurrentStep.Center.X);
                    CurrentStepHit = true;
                }
                else
                {
                    OnMiss?.Invoke(this, null);
                }
            }

            if(direction == DirectionEnum.left)
            {
                if (Rectangle.X + Rectangle.Width < ChangeDirectionAt)
                {
                    if (CurrentStepHit)
                    {
                        ChangeDirection();
                        GetNewChangeDirectionAt(trackEngine.Map.CurrentStep);
                        CurrentStepHit = false;
                    }
                    else
                    {
                        OnOutOfBounds?.Invoke(this, null);
                    }
                }
            }
            else if (direction == DirectionEnum.right)
            {
                if (position.X > ChangeDirectionAt)
                {
                    if (CurrentStepHit)
                    {
                        ChangeDirection();
                        GetNewChangeDirectionAt(trackEngine.Map.CurrentStep);
                        CurrentStepHit = false;
                    }
                    else
                    {
                        OnOutOfBounds?.Invoke(this, null);
                    }
                }
            }
        }

        public void Move()
        {
            if (!running)
            {
                return;
            }

            position.X += (int)direction * speed;
        }
    }
}
