using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.UiComponents;
using RythmGame.Utils;

namespace RythmGame.GamePlay.Track
{
    public class TrackEngineUI
    {
        private Label scoreLabel;
        private Label accuracyLabel;
        private Label endGameLabel;
        private Label timerLabel;
        public LabelListOverflow ComboHitInfoLabels;

        public TrackEngineUI()
        {
            ComboHitInfoLabels = new LabelListOverflow(new Vector2(Globals.WindowWidth / 2 - 30, Globals.WindowHeight / 2), 2000);
            scoreLabel = new Label("Score: 0", new Vector2(0, 0));
            accuracyLabel = new Label("Acc: 0%", new Vector2(0, 30));           
            timerLabel = new Label("0");
            timerLabel.Position = new Vector2(Globals.WindowWidth / 2 - timerLabel.Size.X / 2, Globals.WindowHeight / 2 - timerLabel.Size.Y / 2 - 50);


            endGameLabel = new Label("You  failed");
            endGameLabel.Position = new Vector2(Globals.WindowWidth / 2 - endGameLabel.Size.X / 2, Globals.WindowHeight / 2 - endGameLabel.Size.Y / 2);
        }

        public void Draw(TrackEngine trackEngine, SpriteBatch spriteBatch)
        {

            scoreLabel.Text = "Score: " + trackEngine.Score.ToString();
            accuracyLabel.Text = "Acc: " + trackEngine.PerformanceTracker.Accuracy + "%";
            
            scoreLabel.Draw(spriteBatch);
            accuracyLabel.Draw(spriteBatch);
            ComboHitInfoLabels.Draw(spriteBatch);

            if (trackEngine.GameEnded)
            {
                endGameLabel.Draw(spriteBatch);
            }

            if (trackEngine.RunCountdown)
            {
                timerLabel.Text = trackEngine.TimeToStart.ToString();
                timerLabel.Draw(spriteBatch);
            }
        }

        public void Reset()
        {
            ComboHitInfoLabels.Reset();
        }

        public void Update(TrackEngine trackEngine, GameTime gameTime)
        {
            ComboHitInfoLabels.Update(gameTime);
        }

    }
}
