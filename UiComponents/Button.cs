using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.Utils;
using System;

namespace RythmGame.UiComponents
{
    public class Button
    {
        public Texture2D Texture;
        public float Rotation;
        public Vector2 OriginVector;

        public Rectangle Rectangle;

        private Color color;
        private const int MaxColorAlpha = 255;
        private const int MinColorAlpha = 0;
        private const byte AlphaIncrementer = 3;
        private bool shouldAlphaIncrement;

        public bool Enabled;
        public bool Visible;

        public event EventHandler Click;

        public Button(Texture2D texture, Rectangle rectangle)
        {
            Texture = texture;
            Rectangle = rectangle;

            color = new Color(255, 255, 255, 255);
            Enabled = true;
            Visible = true;
        }

        private void RestoreColorAlpha()
        {
            if (color.A >= MaxColorAlpha)
            {
                return;
            }

            color.A += AlphaIncrementer;
        }

        public void Update()
        {
            if (!Enabled) 
            {
                return; 
            }

            var mouseRectangle = new Rectangle(InputHandler.CurrentMouseState.X, InputHandler.CurrentMouseState.Y, 1, 1);

            if (mouseRectangle.Intersects(Rectangle))
            {
                if (InputHandler.LeftClick())
                {
                    Click?.Invoke(null, null);
                }
                UpdateColorAlpha();
            }
            else
            {
                RestoreColorAlpha();
            }
        }

        private void UpdateColorAlpha()
        {
            if (color.A == MaxColorAlpha)
            { 
                shouldAlphaIncrement = false; 
            }
            if (color.A == MinColorAlpha)
            {
                shouldAlphaIncrement = true;
            }
            if (shouldAlphaIncrement)
            {
                color.A += AlphaIncrementer;
            }
            else
            {
                color.A -= AlphaIncrementer;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible) 
            {
                return;
            }
                
            spriteBatch.Draw(Texture, Rectangle, null, color, Rotation, OriginVector, SpriteEffects.None, 0);
        }
    }
}
