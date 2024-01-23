using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmGame.Utils;

namespace RythmGame.GamePlay
{
    public class GameEngine
    {
        private TrackEngine trackEngine;

        public GameEngine()
        {
            trackEngine = new TrackEngine("gods");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            trackEngine.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            if (InputHandler.IsKeyPressed(Keys.R))
            {
                trackEngine.InitGame();
            }

            trackEngine.Update(gameTime);
        }
    }
}
