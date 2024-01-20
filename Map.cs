using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using RythmGame.GameObjects;

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
            int posY = steps.Count == 0 ? Configuration.WindowHeight - 32 - 50 : steps[steps.Count - 1].Rectangle.Y - 32;
            steps.Add(new Step()
            {
                Rectangle = new Rectangle(posX, posY, 6, 32)
            });
        }

        public void Initialize()
        {
            AddStep(Configuration.WindowWidth - 288);
            AddStep(256);
            AddStep(Configuration.WindowWidth - 288);
            AddStep(256);
            AddStep(Configuration.WindowWidth - 288);
            AddStep(256);
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
