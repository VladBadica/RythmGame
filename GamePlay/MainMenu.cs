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
        private Label mapEditor;

        public event EventHandler PlayClicked;
        public event EventHandler MapEditorClicked;

        public MainMenu()
        {
            trackBall = new TrackBall(3);
            trackBall.Start();

            mapEditor = new Label("Editor");
            mapEditor.Position = new Vector2(Globals.WindowWidth / 2 - 90, trackBall.Rectangle.Y - 20);
            playLabel = new Label("Play");
            playLabel.Position = new Vector2(Globals.WindowWidth / 2, trackBall.Rectangle.Y - 20);
            exitLabel = new Label("Exit");
            exitLabel.Position = new Vector2(Globals.WindowWidth / 2 + 85, trackBall.Rectangle.Y - 20);

            background = AssetManager.GetTexture("mainMenu");
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.White);
            mapEditor.Draw();
            playLabel.Draw();
            exitLabel.Draw();
            trackBall.Draw();
        }

        public void Initialize()
        {
            SoundPlayer.PlaySong("mainMenuMusic");
            trackBall.Reset();
        }

        public void Update()
        {
            if (InputHandler.IsKeyPressed(Keys.Escape))
            {
                Environment.Exit(0);
            }

            if (InputHandler.IsAnyKeyPressed(new[] { Keys.Space, UserPrefs.Settings.LeftActionKey, UserPrefs.Settings.RightActionKey })
                && (trackBall.Rectangle.Center.X > mapEditor.Position.X && trackBall.Rectangle.Center.X < mapEditor.Position.X + mapEditor.Size.X)) 
            {
                Globals.GameState = Globals.GAME_STATE.MAP_EDITOR;
                SoundPlayer.StopSong();
                MapEditorClicked?.Invoke(this, null);
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

            if (trackBall.Rectangle.Center.X < mapEditor.Position.X + mapEditor.Size.X / 2)
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
