using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RythmGame.Utils;

namespace RythmGame.UiComponents
{
    public class Label
    {
        public string Text;
        public Vector2 Position;

        public bool Visible;

        private bool alwaysVisible;
        private double visibilityTime;
        private double elapsedTime;

        private Color color = Color.Black;
        public SpriteFont Font;
        public Vector2 Size => Font.MeasureString(Text);

        public Label(string text, Vector2 position, double visibilityTime)
        {
            Font = AssetManager.GetSpriteFont("calibri18");
            Text = text;
            Position = position;
            alwaysVisible = false;
            Show(visibilityTime);
        }

        public Label(string text, Vector2 position)
        {
            Text = text;
            Position = position;
            Visible = true;
            alwaysVisible = true;
            visibilityTime = -1;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
            {
                return;
            }

            spriteBatch.DrawString(Font, Text, Position, color);
        }

        public void LoadContent()
        {
            Font = AssetManager.GetSpriteFont("calibri18");
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
                Visible = false;
                elapsedTime = 0;
            }

            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
        }


    }
}
