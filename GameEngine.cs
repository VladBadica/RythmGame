using Microsoft.Xna.Framework.Graphics;
using RythmGame.GamePlay;
using RythmGame.GamePlay.Track;
using RythmGame.MapEditor;
using RythmGame.Utils;

namespace RythmGame
{
    public class GameEngine
    {
        private TrackEngine trackEngine;
        public MainMenu mainMenu;
        public SelectionScreen selectionScreen;
        public MapEditorScreen mapEditor;

        public GameEngine()
        {
            mapEditor = new MapEditorScreen();
            mainMenu = new MainMenu();
            Globals.GameState = Globals.GAME_STATE.MAIN_MENU;

            mainMenu.Initialize();
            mainMenu.PlayClicked += (sender, args) =>
            {
                selectionScreen.Initialize();
            };
            mainMenu.MapEditorClicked += (sender, args) =>
            {
                mapEditor.Initialize();
            };

            selectionScreen = new SelectionScreen();
            selectionScreen.StartTrack += (sender, args) =>
            {
                SelectionScreen selected = (SelectionScreen)sender;
                trackEngine.Map = selected.Maps[selected.CurrentIndex];
                trackEngine.InitGame();
            };
            selectionScreen.GoBack += (sender, args) =>
            {
                mainMenu.Initialize();
            };

            trackEngine = new TrackEngine();
            trackEngine.GoBack += (sender, args) =>
            {
                selectionScreen.Initialize();
            };
        }

        public void Draw()
        {
            switch (Globals.GameState)
            {
                case Globals.GAME_STATE.MAIN_MENU:
                    {
                        mainMenu.Draw();
                        break;
                    }
                case Globals.GAME_STATE.MAP_EDITOR:
                    {
                        mapEditor.Draw();
                        break;
                    }
                case Globals.GAME_STATE.SELECTION_SCREEN:
                    {
                        selectionScreen.Draw();
                        break;
                    }
                case Globals.GAME_STATE.PLAYING:
                    {
                        trackEngine.Draw();
                        break;
                    }
                default: break;
            }
        }

        public void Update()
        {
            switch (Globals.GameState)
            {
                case Globals.GAME_STATE.MAIN_MENU:
                    {
                        mainMenu.Update();
                        break;
                    }
                case Globals.GAME_STATE.MAP_EDITOR:
                    {
                        mapEditor.Update();
                        break;
                    }
                case Globals.GAME_STATE.SELECTION_SCREEN:
                    {
                        selectionScreen.Update();
                        break;
                    }
                case Globals.GAME_STATE.PLAYING:
                    {
                        trackEngine.Update();
                        break;
                    }
                default: break;
            }
        }
    }
}
