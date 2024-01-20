using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmGame.GameObjects;
using RythmGame.Utils;

namespace RythmGame
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private TrackBall trackBall;
        private Map map;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            map = new Map();
            trackBall = new TrackBall();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap);

            trackBall.Draw(spriteBatch);
            map.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Initialize()
        {
            AssetManager.Initialize(Content);
            trackBall.Initialize();
            map.Initialize();

            graphics.PreferredBackBufferHeight = Configuration.WindowHeight;
            graphics.PreferredBackBufferWidth = Configuration.WindowWidth;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            trackBall.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputHandler.Update();
            trackBall.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) 
            {
                Exit();
            }

            base.Update(gameTime);
        }
    }
}