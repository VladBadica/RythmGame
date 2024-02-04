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
            trackCompleteLabel.Position = new Vector2(Globals.WindowWidth / 2 - trackCompleteLabel.Size.X / 2, 150);

            trackFailedLabel = new Label("Track Failed");
            trackFailedLabel.Position = new Vector2(Globals.WindowWidth / 2 - trackFailedLabel.Size.X / 2, 150);
            
            scoreLabel = new Label("Your Score: 0");
            scoreLabel.Position = new Vector2(Globals.WindowWidth / 2 - scoreLabel.Size.X / 2, trackFailedLabel.Position.Y + trackFailedLabel.Size.Y + 10);
             
            accuracyLabel = new Label("Accuracy: 0%");
            accuracyLabel.Position = new Vector2(Globals.WindowWidth / 2 - accuracyLabel.Size.X / 2, scoreLabel.Position.Y + scoreLabel.Size.Y + 10);

            infolabel = new Label("Press \'R\' to try again");
            infolabel.Position = new Vector2(Globals.WindowWidth / 2 - infolabel.Size.X / 2, Globals.WindowHeight - 30);

        }

        public void Draw(TrackEngine trackEngine, SpriteBatch spriteBatch)
        {
            scoreLabel.Text = $"Your Score: {trackEngine.Score}";
            scoreLabel.Position = new Vector2(Globals.WindowWidth / 2 - scoreLabel.Size.X / 2, trackFailedLabel.Position.Y + trackFailedLabel.Size.Y + 10);

            accuracyLabel.Text = $"Accuracy: {trackEngine.PerformanceTracker.Accuracy}%";
            accuracyLabel.Position = new Vector2(Globals.WindowWidth / 2 - accuracyLabel.Size.X / 2, scoreLabel.Position.Y + scoreLabel.Size.Y + 10);

            if (trackEngine.LevelCompleted)
            {
                trackCompleteLabel.Draw(spriteBatch);
            }
            else
            {
                trackFailedLabel.Draw(spriteBatch);
            }
            scoreLabel.Draw(spriteBatch);
            accuracyLabel.Draw(spriteBatch); 
            infolabel.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
