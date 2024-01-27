using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using RythmGame.Utils;
using System;

namespace RythmGame.GamePlay.Track
{
    public class TrackEngine
    {
        public string SongName;
        public int Score;
        public bool ShowCountdown;
        public int TimeToStart;
        public bool Running;
        public bool GameEnded;
        public bool GamePaused;
        public TrackBall TrackBall;
        public float TrackBallSpeed = 4;
        public Map Map;
        public TrackEngineUI UI;
        public EndGameUI EndGameUI;

        public double ElapsedTimeToStart;
        public PerformanceTracker PerformanceTracker;

        public event EventHandler GoBack;

        public TrackEngine()
        {
            ShowCountdown = true;
            Running = false;
            TrackBall = new TrackBall(TrackBallSpeed);
            PerformanceTracker = new PerformanceTracker();
            UI = new TrackEngineUI();
            EndGameUI = new EndGameUI();
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            if (!GameEnded)
            {
                Map.Draw(spriteBatch);
                TrackBall.Draw(spriteBatch);
                UI.Draw(this, spriteBatch);               
            }
            else
            {
                EndGameUI.Draw(this, spriteBatch);
            }
        }

        private void HandleActionKeysPressed()
        {
            if (TrackBall.Rectangle.Intersects(Map.CurrentStep))
            {
                PerformanceTracker.AddHitAccuracy(TrackBall.Rectangle.Width, TrackBall.CenterX, Map.CurrentStep.Center.X);
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
            else
            {
                ShowCountdown = false;
                StartTrack();
            }
        }

        private void StartTrack()
        {
            TrackBall.Start();
            SoundPlayer.PlaySong(Map.SongFile);
        }

        public void InitGame()
        {
            SoundPlayer.StopSong();
            Score = 0;
            ElapsedTimeToStart = 0;
            TimeToStart = 3;
            Running = true;
            ShowCountdown = true;
            GameEnded = false;
            Map.Reset();
            TrackBall.Reset();
            PerformanceTracker.Reset();
            UI.Reset();
        }

        private void PauseGame()
        {
            GamePaused = true;
            Running = false;
            TrackBall.Stop();
            SoundPlayer.PauseSong();
        }

        private void ContinueGame()
        {
            GamePaused = false;
            Running = true;
            TrackBall.Start();
            SoundPlayer.ContinueSong();
        }

        private void TrackFailed()
        {
            GameEnded = true;
            Running = false;

            SoundPlayer.StopSong();
            SoundPlayer.PlayEffect(SoundPlayer.SoundEffects.TrackFailed);
        }

        public void Update(GameTime gameTime)
        {
            UI.Update(this, gameTime);

            if (InputHandler.IsKeyPressed(Keys.Escape))
            {
                if (GamePaused)
                {
                    ContinueGame();
                }
                else
                {
                    PauseGame();
                }
            }

            if (InputHandler.IsKeyPressed(Keys.R))
            {
                InitGame();
            }

            if (Running)
            {                
                if (ShowCountdown)
                {
                    UpdateCountDown(gameTime);
                }
                else
                {
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
    }
}
