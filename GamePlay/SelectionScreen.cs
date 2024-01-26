﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using RythmGame.UiComponents;
using RythmGame.Utils;
using System.Collections.Generic;
using System.IO;
using RythmGame.GamePlay.Track;
using Microsoft.Xna.Framework.Input;
using System;

namespace RythmGame.GamePlay
{
    public class SelectionScreen
    {
        public List<Map> Maps;
        public int CurrentIndex;

        private Texture2D mapSelectionTextureLeft;
        private Rectangle mapSelectionRectangleLeft;

        private Texture2D mapSelectionTextureRight;
        private Rectangle mapSelectionRectangleRight;

        private Texture2D mapSelectionTexture;
        private Rectangle mapSelectionRectangle;
        private Label mapName;
        private Label mapDuration;
        private Label mapAuthor;
        public event EventHandler StartTrack;


        public SelectionScreen()
        {
            CurrentIndex = 0;
            Maps = GetAllMaps();
            mapSelectionTexture = AssetManager.GetTexture("mapSelectionRectangle");
            mapSelectionRectangle = new Rectangle(Globals.WindowWidth / 2 - mapSelectionTexture.Width / 2, Globals.WindowHeight / 2 - mapSelectionTexture.Height / 2, mapSelectionTexture.Width, mapSelectionTexture.Height);
            mapName = new Label("Map Name: " + Maps[CurrentIndex].MapName);
            mapName.Position = new Vector2(mapSelectionRectangle.X + mapSelectionRectangle.Width / 2 - mapName.Size.X / 2, mapSelectionRectangle.Y + 10);
            mapDuration = new Label("Duration: " + Maps[CurrentIndex].Song.Duration.ToString(@"mm\:ss"));
            mapDuration.Position = new Vector2(mapSelectionRectangle.X + mapSelectionRectangle.Width / 2 - mapName.Size.X / 2, mapName.Position.Y + mapName.Size.Y);
            mapAuthor = new Label("Made By: " + Maps[CurrentIndex].Author);
            mapAuthor.Position = new Vector2(mapSelectionRectangle.X + mapSelectionRectangle.Width / 2 - mapDuration.Size.X / 2, mapDuration.Position.Y + mapDuration.Size.Y);

            mapSelectionTextureLeft = AssetManager.GetTexture("mapSelectionRectangle");
            mapSelectionRectangleLeft = new Rectangle(Globals.WindowWidth / 5 - mapSelectionTextureLeft.Width / 4, Globals.WindowHeight / 2 - mapSelectionTextureLeft.Height / 4, mapSelectionTextureLeft.Width / 2, mapSelectionTextureLeft.Height / 2);

            mapSelectionTextureRight = AssetManager.GetTexture("mapSelectionRectangle");
            mapSelectionRectangleRight = new Rectangle(Globals.WindowWidth - Globals.WindowWidth / 5 - mapSelectionTextureRight.Width / 4, Globals.WindowHeight / 2 - mapSelectionTextureRight.Height / 4, mapSelectionTextureRight.Width / 2, mapSelectionTextureRight.Height / 2);
        }

        public List<Map> GetAllMaps()
        {
            List<Map> maps = new List<Map>();
            string[] mapFiles = Directory.GetFiles($"{Directory.GetCurrentDirectory()}\\maps");
            for (int i = 0; i < mapFiles.Length; i++)
            {
                Map map = JsonConvert.DeserializeObject<Map>(File.ReadAllText(mapFiles[i]));
                map.Song = AssetManager.GetSong(map.SongFile);
                maps.Add(map);

                mapFiles[i] = mapFiles[i].Split("\\")[mapFiles[i].Split("\\").Length - 1].Split('.')[0];
            }

            return maps;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mapSelectionTexture, mapSelectionRectangle, Color.White);
            spriteBatch.Draw(mapSelectionTextureLeft, mapSelectionRectangleLeft, Color.White);
            spriteBatch.Draw(mapSelectionTextureRight, mapSelectionRectangleRight, Color.White);

            mapName.Text = "Map Name: " + Maps[CurrentIndex].MapName;
            mapName.Position = new Vector2(mapSelectionRectangle.X + mapSelectionRectangle.Width / 2 - mapName.Size.X / 2, mapSelectionRectangle.Y + 10);
            mapDuration.Text = "Duration: " + Maps[CurrentIndex].Song.Duration.ToString(@"mm\:ss");
            mapDuration.Position = new Vector2(mapSelectionRectangle.X + mapSelectionRectangle.Width / 2 - mapName.Size.X / 2, mapName.Position.Y + mapName.Size.Y);
            mapAuthor.Text = "Made By: " + Maps[CurrentIndex].Author;
            mapAuthor.Position = new Vector2(mapSelectionRectangle.X + mapSelectionRectangle.Width / 2 - mapDuration.Size.X / 2, mapDuration.Position.Y + mapDuration.Size.Y);

            mapName.Draw(spriteBatch);
            mapDuration.Draw(spriteBatch);
            mapAuthor.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            if (InputHandler.IsKeyPressed(Keys.Z))
            {
                if(CurrentIndex != 0)
                {
                    CurrentIndex--;
                }
            }

            if (InputHandler.IsKeyPressed(Keys.X))
            {
                if (CurrentIndex != Maps.Count - 1)
                {
                    CurrentIndex++;
                }
            }

            if (InputHandler.IsKeyPressed(Keys.Space))
            {
                StartTrack?.Invoke(this, null);
                Globals.GameState = Globals.GAME_STATE.PLAYING;
            }
        }
    }
}
