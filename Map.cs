﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using RythmGame.GameObjects;
using RythmGame.Utils;

namespace RythmGame
{
    public class Map
    {
        private List<Step> steps;

        public Map()
        {
            steps = new List<Step>();
        }

        public Step CurrentStep => steps[0];
        private void AddStep(int posX)
        {
            int posY = steps.Count == 0 ? UserPrefs.Settings.WindowHeight - 32 - 50 : steps[steps.Count - 1].Rectangle.Y - 32;
            steps.Add(new Step()
            {
                Rectangle = new Rectangle(posX, posY, 6, 32)
            });
        }

        public void Initialize()
        {
            steps.Clear();
            for(int i = 0; i < 7; i++)
            {
                AddStep(420);
                AddStep(UserPrefs.Settings.WindowWidth - 420);
            }
            for (int i = 0; i < 5; i++)
            {
                AddStep(460);
                AddStep(UserPrefs.Settings.WindowWidth - 460);
            }
            for (int i = 0; i < 10; i++)
            {
                AddStep(440);
                AddStep(UserPrefs.Settings.WindowWidth - 440);
            }
            for (int i = 0; i < 300; i++)
            {
                AddStep(450);
                AddStep(UserPrefs.Settings.WindowWidth - 450);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            steps.ForEach(step => step.Draw(spriteBatch));
        }

        public void NextStep()
        {
            steps.RemoveAt(0);
            steps.ForEach(step => step.Rectangle.Y += 32);
        }
    }
}
