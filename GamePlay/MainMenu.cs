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
        private TrackBall trackBall;
        private Label playLabel;
        private Label exitLabel;

        public MainMenu()
        {
            trackBall = new TrackBall(3);
            trackBall.Start();

            playLabel = new Label("Play", new Vector2(Globals.WindowWidth / 2 - 60, trackBall.Rectangle.Y - 20));
            exitLabel = new Label("Exit", new Vector2(Globals.WindowWidth / 2 + 60, trackBall.Rectangle.Y - 20));

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playLabel.Draw(spriteBatch);
            exitLabel.Draw(spriteBatch);
            trackBall.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            if (InputHandler.IsAnyKeyPressed(new[] { Keys.Space, UserPrefs.Settings.LeftActionKey, UserPrefs.Settings.RightActionKey })
                && (trackBall.CenterX > playLabel.Position.X && trackBall.CenterX < playLabel.Position.X + playLabel.Size.X)) 
            {
                Globals.GameState = Globals.GAME_STATE.SELECTION_SCREEN;
            }
            if (InputHandler.IsAnyKeyPressed(new[] { Keys.Space, UserPrefs.Settings.LeftActionKey, UserPrefs.Settings.RightActionKey })
                && (trackBall.CenterX > exitLabel.Position.X && trackBall.CenterX < exitLabel.Position.X + exitLabel.Size.X))
            {
                Environment.Exit(0);
            }

            trackBall.Update(gameTime);

            if (trackBall.CenterX < playLabel.Position.X + playLabel.Size.X / 2)
            {
                trackBall.ChangeDirection();
            }
            if (trackBall.CenterX > exitLabel.Position.X + exitLabel.Size.X / 2)
            {
                trackBall.ChangeDirection();
            }
        }
    }
}
