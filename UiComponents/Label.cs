using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RythmGame.Utils;
using System;

namespace RythmGame.UiComponents
{
    public class Label
    {
        public string Text;
        public Vector2 Position;
        public float Scale;

        public bool Visible;

        private bool alwaysVisible;
        private double visibilityTime;
        private double elapsedTime;

        private Color color = Color.Black;
        public byte Alpha
        {
            get
            {
                return color.A;
            }
            set
            {
                color.A = value;
            }
        }
        public SpriteFont Font;
        public Vector2 Size => Font.MeasureString(Text);
        public event EventHandler TimedLabelHidden;

        public Label(string text, Vector2 position, double visibilityTime)
        {
            Font = AssetManager.GetSpriteFont("calibri18");
            Text = text;
            Position = position;
            alwaysVisible = false;
            Show(visibilityTime);
            Scale = 1f;
        }

        public Label(string text)
        {
            Font = AssetManager.GetSpriteFont("calibri18");
            Text = text;
            Visible = true;
            alwaysVisible = true;
            visibilityTime = -1;
            Scale = 1f;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
            {
                return;
            }

            spriteBatch.DrawString(Font, Text, Position, color, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }

        public void Show(double timeVisible)
        {
            Visible = true;
            visibilityTime = timeVisible;
            elapsedTime = 0;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!Visible)
            {
                return;
            }

            if (elapsedTime >= visibilityTime && !alwaysVisible)
            {
                TimedLabelHidden.Invoke(this, null);
                Visible = false;
                elapsedTime = 0;
            }

            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

    }
}
