using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RythmGame.GameObjects;
using RythmGame.Utils;
using RythmGame.UiComponents;

namespace RythmGame.GamePlay.Track
{
    public class TrackEngine
    {
        public string SongName;
        public int Score;
        public bool RunCountdown;
        public int TimeToStart;
        public bool Running;
        public bool GameEnded;
        public TrackBall TrackBall;
        public Map Map;
        public TrackEngineUI UI;

        public double ElapsedTimeToStart;
        public PerformanceTracker PerformanceTracker;

        public TrackEngine(string SongName)
        {
            this.SongName = SongName;
            RunCountdown = true;
            Running = false;
            TrackBall = new TrackBall();
            Map = new Map();
            PerformanceTracker = new PerformanceTracker();
            UI = new TrackEngineUI();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);
            TrackBall.Draw(spriteBatch);
            UI.Draw(this, spriteBatch);
        }

        private void HandleActionKeysPressed()
        {
            if (TrackBall.Rectangle.Intersects(Map.CurrentStep.Rectangle))
            {
                PerformanceTracker.AddHitAccuracy(TrackBall.Rectangle.Width, TrackBall.CenterX, Map.CurrentStep.CenterX);
                Score += PerformanceTracker.GetLastHitScore();

                Map.NextStep();

                SoundPlayer.PlayEffect(SoundPlayer.SoundEffects.StepHit);
                TrackBall.ChangeDirection();
                UI.ComboHitInfoLabels.AddLabel(PerformanceTracker.GetLastHitInfoLabel());
            }
            else
            {
                TrackFailed();
            }
        }

        private void UpdateCountDown(GameTime gameTime)
        {
            if (TimeToStart > 0)
            {
                ElapsedTimeToStart += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (ElapsedTimeToStart > 1000)
                {
                    TimeToStart -= 1;
                    ElapsedTimeToStart = 0;
                }
            }
            if (TimeToStart <= 0)
            {
                RunCountdown = false;
                StartTrack();
            }
        }

        private void StartTrack()
        {
            TrackBall.Start();
            SoundPlayer.PlaySong(SongName);
        }

        //Starts Game and CountdownTimer
        public void InitGame()
        {
            Score = 0;
            ElapsedTimeToStart = 0;
            TimeToStart = 3;
            Running = true;
            RunCountdown = true;
            GameEnded = false;
            Map.Reset();
            PerformanceTracker.Reset();
            UI.Reset();
        }

        private void TrackFailed()
        {
            GameEnded = true;
            Running = false;
            UI.Reset();

            SoundPlayer.StopSong();
            SoundPlayer.PlayEffect(SoundPlayer.SoundEffects.TrackFailed);
        }

        public void Update(GameTime gameTime)
        {
            UI.Update(this, gameTime);
            if (!Running)
            {
                return;
            }

            if (RunCountdown)
            {
                UpdateCountDown(gameTime);
                return;
            }

            TrackBall.Update(gameTime);

            if (TrackBall.CenterX < 100 || TrackBall.CenterX > Globals.WindowWidth - 100)
            {
                TrackFailed();
            }

            if (InputHandler.IsKeyPressed(UserPrefs.Settings.LeftActionKey) || InputHandler.IsKeyPressed(UserPrefs.Settings.RightActionKey))
            {
                HandleActionKeysPressed();
            }

        }
    }
}
