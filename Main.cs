using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmGame.Utils;

namespace RythmGame
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameEngine gameEngine;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            gameEngine = new GameEngine();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.HotPink);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap);
            gameEngine.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Initialize()
        {
            AssetManager.Initialize(Content);
            gameEngine.StartGame();

            graphics.PreferredBackBufferHeight = Configuration.WindowHeight;
            graphics.PreferredBackBufferWidth = Configuration.WindowWidth;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameEngine.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputHandler.Update();
            gameEngine.Update(gameTime);

            if (InputHandler.IsKeyPressed(Keys.R))
            {
                gameEngine.StartGame();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }
    }
}