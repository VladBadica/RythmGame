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

            selectionScreen = new SelectionScreen();
            selectionScreen.StartTrack += (object sender, EventArgs args) => {
                SelectionScreen selected = (SelectionScreen)sender;
                trackEngine.Map = selected.Maps[selected.CurrentIndex];
                trackEngine.InitGame();
            };
            selectionScreen.GoBack += (object sender, EventArgs args) => {
                mainMenu.Initialize();
            };

            trackEngine = new TrackEngine();
            trackEngine.GoBack += (object sender, EventArgs args) => {
                selectionScreen.Initialize();
            };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (Globals.GameState)
            {
                case Globals.GAME_STATE.MAIN_MENU:
                    {
                        mainMenu.Draw(spriteBatch);
                        break;
                    }
                case Globals.GAME_STATE.SELECTION_SCREEN:
                    {
                        selectionScreen.Draw(spriteBatch);
                        break;
                    }
                case Globals.GAME_STATE.PLAYING:
                    {
                        trackEngine.Draw(spriteBatch);
                        break;
                    }
                default: break;                   
            }
        }

        public void Update(GameTime gameTime)
        {
            switch (Globals.GameState) 
            {
                case Globals.GAME_STATE.MAIN_MENU:
                    {
                        mainMenu.Update(gameTime);
                        break;
                    }
                case Globals.GAME_STATE.SELECTION_SCREEN:
                    {
                        selectionScreen.Update(gameTime);
                        break;
                    }
                case Globals.GAME_STATE.PLAYING:
                    {
                        trackEngine.Update(gameTime);
                        break;
                    }
                default: break;                    
            }
        }
    }
}
