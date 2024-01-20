using Microsoft.Xna.Framework;
using RythmGame.Utils;
using Microsoft.Xna.Framework.Input;
using RythmGame.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace RythmGame
{
    public class GameEngine
    {
        private TrackBall trackBall;
        private Map map;


        public GameEngine()
        {
            trackBall = new TrackBall();
            map = new Map();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            trackBall.Draw(spriteBatch);
            map.Draw(spriteBatch);
        }

        public void Initialize()
        {
            trackBall.Initialize();
            map.Initialize();
        }

        public void LoadContent()
        {
            trackBall.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (InputHandler.IsKeyPressed(Keys.Space))
            {

            }
        }
    }
}
