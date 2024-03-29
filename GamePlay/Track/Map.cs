﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using RythmGame.Utils;
using Microsoft.Xna.Framework.Media;
using System;

namespace RythmGame.GamePlay.Track
{
    public class Map
    {
        public string MapName;
        public string SongFile;
        public string Author;
        public string StepTextureString = "stepLine";
        public Texture2D BackgroundTexture;
        public Texture2D StepTexture;
        public Song Song;
        public List<Rectangle> StepsTemplate;
        private List<Rectangle> steps;
        public int StepsCount => steps.Count;

        public Map()
        {
            steps = new List<Rectangle>();
            StepTexture = AssetManager.GetTexture(StepTextureString);
            BackgroundTexture = AssetManager.GetTexture("retroBackground2");
        }

        public Rectangle CurrentStep => steps[0];

        public void Draw()
        {
            Globals.SpriteBatch.Draw(BackgroundTexture, new Rectangle(0, 0, Globals.WindowWidth, Globals.WindowHeight), Color.White);
            steps.ForEach(step => Globals.SpriteBatch.Draw(StepTexture, step, Color.White));
        }

        public void Reset()
        {
            steps = new List<Rectangle>(StepsTemplate);
        }

        public void NextStep()
        {
            steps.RemoveAt(0);
            for (int i = 0; i < steps.Count; i++)
            {
                Rectangle newStep = steps[i];
                newStep.Y += 48;
                steps[i] = newStep;
            }
        }
    }
}
