using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.UiComponents;
using RythmGame.Utils;

namespace RythmGame.GamePlay.Track
{
    public class EndGameUI
    {
        private Label trackCompleteLabel;
        private Label trackFailedLabel;
        private Label scoreLabel;
        private Label accuracyLabel;
        private Label infolabel;

        public EndGameUI()
        {
            trackCompleteLabel = new Label("Track Completed");
            trackCompleteLabel.Position = new Vector2(Globals.WindowWidth / 2 - (int)trackCompleteLabel.Size.X / 2, 150);

            trackFailedLabel = new Label("Track Failed");
            trackFailedLabel.Position = new Vector2(Globals.WindowWidth / 2 - (int)trackFailedLabel.Size.X / 2, 150);
            
            scoreLabel = new Label("Your Score: 0");
            scoreLabel.Position = new Vector2(Globals.WindowWidth / 2 - (int)scoreLabel.Size.X / 2, 170);
             
            accuracyLabel = new Label("Accuracy: 0%");
            accuracyLabel.Position = new Vector2(Globals.WindowWidth / 2 - (int)accuracyLabel.Size.X / 2, 190);

            infolabel = new Label("Press \'R\' to try again");
            infolabel.Position = new Vector2(Globals.WindowWidth / 2 - (int)infolabel.Size.X / 2, Globals.WindowHeight - 30);

        }

        public void Draw(TrackEngine trackEngine)
        {
            scoreLabel.Text = $"Your Score: {trackEngine.Score}";
            scoreLabel.Position = new Vector2(Globals.WindowWidth / 2 - (int)scoreLabel.Size.X / 2, 170);

            accuracyLabel.Text = $"Accuracy: {trackEngine.PerformanceTracker.Accuracy}%";
            accuracyLabel.Position = new Vector2(Globals.WindowWidth / 2 - (int)accuracyLabel.Size.X / 2, 190);

            if (trackEngine.LevelCompleted)
            {
                trackCompleteLabel.Draw();
            }
            else
            {
                trackFailedLabel.Draw();
            }
            scoreLabel.Draw();
            accuracyLabel.Draw();
            infolabel.Draw();
        }

        public void Update()
        {

        }
    }
}
