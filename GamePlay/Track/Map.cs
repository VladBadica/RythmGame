using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using RythmGame.Utils;
using Microsoft.Xna.Framework.Media;

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

        public Map()
        {
            steps = new List<Rectangle>();
            StepTexture = AssetManager.GetTexture(StepTextureString);
            BackgroundTexture = AssetManager.GetTexture("retroBackground");
        }

        public Rectangle CurrentStep => steps[0];

        private void AddStep(int posX)
        {
            int posY = steps.Count == 0 ? Globals.WindowHeight - 32 - 50 : steps[steps.Count - 1].Y - 32;
            steps.Add(new Rectangle(posX, posY, 3, 32));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, new Rectangle(0, 0, Globals.WindowWidth, Globals.WindowHeight), Color.White);
            steps.ForEach(step => spriteBatch.Draw(StepTexture, step, Color.White));
        }

        public void Reset()
        {
            steps = new List<Rectangle>(StepsTemplate);
            /*for (int i = 0; i < 3; i++)
            {
                AddStep(420);
                AddStep(Globals.WindowWidth - 420);
            }
            AddStep(470);
            AddStep(Globals.WindowWidth - 470);
            AddStep(470);
            for (int i = 0; i < 3; i++)
            {
                AddStep(Globals.WindowWidth - 420);
                AddStep(420);
            }
            AddStep(Globals.WindowWidth - 490);
            for (int i = 0; i < 5; i++)
            {
                AddStep(460);
                AddStep(Globals.WindowWidth - 460);
            }
            for (int i = 0; i < 10; i++)
            {
                AddStep(440);
                AddStep(Globals.WindowWidth - 440);
            }
            for (int i = 0; i < 300; i++)
            {
                AddStep(450);
                AddStep(Globals.WindowWidth - 450);
            }*/
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
