﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.HotPink);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap);
            gameEngine.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Initialize()
        {
            UserPrefs.Initialize();
            AssetManager.Initialize(Content);

            graphics.PreferredBackBufferHeight = Globals.WindowHeight;
            graphics.PreferredBackBufferWidth = Globals.WindowWidth;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = spriteBatch;
            Globals.GraphicsDevice = GraphicsDevice;

            SoundPlayer.LoadContent();

            gameEngine = new GameEngine();
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.GameTime = gameTime;

            InputHandler.Update();
            gameEngine.Update();

            base.Update(gameTime);
        }
    }
}