using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using RythmGame.Utils;
using Microsoft.Xna.Framework.Media;
using System;

namespace RythmGame.MapEditor
{
    public class Map
    {
        public string MapName;
        public string SongFile;
        public string Author;
        public string StepTextureString = "stepLine";
        public Texture2D StepTexture;
        public Song Song;
        public List<Rectangle> StepsTemplate;
        private List<Rectangle> steps;
        public int StepsCount => steps.Count;
        public int StepIndex = 0;

        public Map()
        {
            steps = new List<Rectangle>();
            StepTexture = AssetManager.GetTexture(StepTextureString);
        }

        public Rectangle CurrentStep => steps[0];
        public int NextStepPosY => steps.Count == 0 ? Globals.TrackBallStartY : steps[^1].Y - Globals.StepHeight;

        public bool AllowNextStepPosX(int stepX)
        {
            if (steps.Count == 0 && stepX < Globals.TrackBallStartX)
            {
                return true;
            }
            else if (steps.Count != 0 && steps.Count % 2 == 0 && stepX < steps[^1].X)
            {
                return true;
            }
            else if (steps.Count % 2 == 1 && stepX > steps[^1].X)
            {
                return true;
            }
            return false;
        }

        public void AddStep(int stepX)
        {
            if(AllowNextStepPosX(stepX))
            {
                steps.Add(new Rectangle(stepX, NextStepPosY, Globals.StepWidth, Globals.StepHeight));
            }         
        }

        public void Draw()
        {
            steps.ForEach(step => Globals.SpriteBatch.Draw(StepTexture, new Rectangle(step.X, step.Y + Globals.StepHeight * StepIndex, step.Width, step.Height), Color.White));
        }

        public void NextStep()
        {
            if(StepIndex < StepsCount)
            {
                StepIndex++;
            }
        }

        public void PreviousStep()
        {
            if (StepIndex > 0)
            {
                StepIndex--;
            }
        }

    }
}
