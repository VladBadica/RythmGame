using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmGame.GamePlay.Track;
using RythmGame.UiComponents;
using RythmGame.Utils;
using System;

namespace RythmGame.GamePlay
{
    public class MainMenu
    {
        private Texture2D background;
        private TrackBall trackBall;
        private Label playLabel;
        private Label exitLabel;

        public event EventHandler PlayClicked;

        public MainMenu()
        {
            trackBall = new TrackBall(3);
            trackBall.Start();

            playLabel = new Label("Play");
            playLabel.Position = new Vector2(Globals.WindowWidth / 2 - 60, trackBall.Rectangle.Y - 20);
            exitLabel = new Label("Exit");
            exitLabel.Position = new Vector2(Globals.WindowWidth / 2 + 60, trackBall.Rectangle.Y - 20);

            background = AssetManager.GetTexture("mainMenu");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);
            playLabel.Draw(spriteBatch);
            exitLabel.Draw(spriteBatch);
            trackBall.Draw(spriteBatch);
        }

        public void Initialize()
        {
            SoundPlayer.PlaySong("mainMenuMusic");
            trackBall.Reset();
        }

        public void Update(GameTime gameTime)
        {
            if (InputHandler.IsKeyPressed(Keys.Escape))
            {
                Environment.Exit(0);
            }

            if (InputHandler.IsAnyKeyPressed(new[] { Keys.Space, UserPrefs.Settings.LeftActionKey, UserPrefs.Settings.RightActionKey })
                && (trackBall.Rectangle.Center.X > playLabel.Position.X && trackBall.Rectangle.Center.X < playLabel.Position.X + playLabel.Size.X)) 
            {
                Globals.GameState = Globals.GAME_STATE.SELECTION_SCREEN;
                SoundPlayer.StopSong();
                PlayClicked?.Invoke(this, null);
            }
            if (InputHandler.IsAnyKeyPressed(new[] { Keys.Space, UserPrefs.Settings.LeftActionKey, UserPrefs.Settings.RightActionKey })
                && (trackBall.Rectangle.Center.X > exitLabel.Position.X && trackBall.Rectangle.Center.X < exitLabel.Position.X + exitLabel.Size.X))
            {
                Environment.Exit(0);
            }

            trackBall.Move();

            if (trackBall.Rectangle.Center.X < playLabel.Position.X + playLabel.Size.X / 2)
            {
                trackBall.ChangeDirection();
            }
            if (trackBall.Rectangle.Center.X > exitLabel.Position.X + exitLabel.Size.X / 2)
            {
                trackBall.ChangeDirection();
            }
        }
    }
}
