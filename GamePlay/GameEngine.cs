using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.Utils;
using System;

namespace RythmGame.GamePlay
{
    public class GameEngine
    {
        private TrackEngine trackEngine;
        private string[] availableMaps;
        public MainMenu mainMenu;

        public GameEngine()
        {
            availableMaps = MapLoader.GetAllMaps();
            mainMenu = new MainMenu();
            mainMenu.StartPlaying += (object sender, EventArgs args) => { trackEngine.InitGame(); };

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
