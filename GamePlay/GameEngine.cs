using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.GamePlay.Track;
using RythmGame.Utils;
using System;

namespace RythmGame.GamePlay
{
    public class GameEngine
    {
        private TrackEngine trackEngine;
        public MainMenu mainMenu;
        public SelectionScreen selectionScreen;

        public GameEngine()
        {
            mainMenu = new MainMenu();
            //mainMenu.StartPlaying += (object sender, EventArgs args) => { trackEngine.InitGame(); };

            selectionScreen = new SelectionScreen();

            trackEngine = new TrackEngine("gods");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (Globals.GameState)
            {
                case Globals.GAME_STATE.PLAYING:
                    {
                        trackEngine.Draw(spriteBatch);
                        break;
                    }
                case Globals.GAME_STATE.SELECTION_SCREEN:
                    {
                        selectionScreen.Draw(spriteBatch);
                        break;
                    }
                case Globals.GAME_STATE.MAIN_MENU:
                default:
                    {
                        mainMenu.Draw(spriteBatch);
                        break;
                    }
            }
        }

        public void Update(GameTime gameTime)
        {
            switch (Globals.GameState) {
                case Globals.GAME_STATE.PLAYING:
                    {
                        trackEngine.Update(gameTime);
                        break;
                    }
                case Globals.GAME_STATE.SELECTION_SCREEN:
                    {
                        selectionScreen.Update(gameTime);
                        break;
                    }
                case Globals.GAME_STATE.MAIN_MENU:
                default:
                    {
                        mainMenu.Update(gameTime);
                        break;
                    }
            }
           
        }
    }
}
