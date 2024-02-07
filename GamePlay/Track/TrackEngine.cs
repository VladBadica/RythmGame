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
        public bool LevelCompleted;
        public TrackBall TrackBall;
        public float TrackBallSpeed = 4;
        public Map Map;
        public TrackEngineUI UI;
        public EndGameUI EndGameUI;
        public PauseGameUI PauseGameUI;

        public double ElapsedTimeToStart;
        public PerformanceTracker PerformanceTracker;

        public event EventHandler GoBack;

        public TrackEngine()
        {
            LevelCompleted = false;
            ShowCountdown = true;
            Running = false;
            PerformanceTracker = new PerformanceTracker();
            UI = new TrackEngineUI();
            PauseGameUI = new PauseGameUI();
            EndGameUI = new EndGameUI();
            TrackBall = new TrackBall(TrackBallSpeed);

            TrackBall.OnCorrectHit += (sender, currentStepCenterX) =>
            {
                TrackBall senderObj = (TrackBall)sender;

                SoundPlayer.PlayEffect(SoundPlayer.SoundEffects.StepHit);

                PerformanceTracker.AddHitAccuracy(senderObj.Rectangle.Width, senderObj.Rectangle.Center.X, currentStepCenterX);
                Score += PerformanceTracker.GetLastHitScore();
                UI.ComboHitInfoLabels.AddLabel(PerformanceTracker.GetLastHitInfoLabel());

                Map.NextStep();
                if (Map.StepsCount == 0)
                {
                    TrackFinished();
                }
            };
            TrackBall.OnMiss += (sender, args) =>
            {
                TrackFailed();
            };
            TrackBall.OnOutOfBounds += (sender, args) =>
            {
                TrackFailed();
            };
        }

        public void Draw()
        {
            if (!GameEnded)
            {
                Map.Draw();
                TrackBall.Draw();
                UI.Draw(this);
                if (GamePaused)
                {
                    PauseGameUI.Draw();
                }
            }
            else
            {
                EndGameUI.Draw(this);
            }
        }

        private void UpdateCountDown()
        {
            if (TimeToStart > 0)
            {
                ElapsedTimeToStart += Globals.GameTime.ElapsedGameTime.TotalMilliseconds;
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
            TimeToStart = 1;
            LevelCompleted = false;
            Running = true;
            ShowCountdown = true;
            GameEnded = false;
            Map.Reset();
            TrackBall.Reset();
            TrackBall.GetNewChangeDirectionAt(Map.CurrentStep);
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

        private void TrackFinished()
        {
            GameEnded = true;
            Running = false;
            LevelCompleted = true;

            SoundPlayer.StopSong();
            //SoundPlayer.PlayEffect(SoundPlayer.SoundEffects.TrackFailed);
        }

        public void Update()
        {
            UI.Update(this);

            if (InputHandler.IsKeyPressed(Keys.Escape))
            {
                if (GameEnded)
                {
                    GoBack?.Invoke(this, null);
                    Globals.GameState = Globals.GAME_STATE.SELECTION_SCREEN;
                }
                else if (GamePaused)
                {
                    ContinueGame();
                }
                else
                {
                    PauseGame();
                }
            }

            if (InputHandler.IsKeyPressed(Keys.R) && !GamePaused)
            {
                InitGame();
            }

            if (Running)
            {                
                if (ShowCountdown)
                {
                    UpdateCountDown();
                }
                else
                {
                    TrackBall.Update(this);
                }               
            }
        }
    }
}
