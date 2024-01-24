using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RythmGame.GameObjects;
using RythmGame.Utils;
using RythmGame.UiComponents;
using System;

namespace RythmGame.GamePlay
{
    public class TrackEngine
    {
        private string songName;
        private int _score;
        private int score
        {
            get { return _score; }
            set
            {
                _score = value;
                scoreLabel.Text = "Score: " + _score.ToString();
            }
        }
        private bool runCountdown;
        private int _timeToStart;
        private int timeToStart
        {
            get
            {
                return _timeToStart;
            }
            set
            {
                _timeToStart = value;
                timerLabel.Text = _timeToStart.ToString();
            }
        }
        private bool running;
        private TrackBall trackBall;
        private Map map;
        private Label scoreLabel;
        private Label accuracyLabel;
        private Label endGameLabel;
        private Label timerLabel;
        private double elapsedTimeToStart;
        private PerformanceTracker performanceTracker;
        private LabelListOverflow comboHitInfoLabels;
        private Step line1;
        private Step line2;
        private Step line3;

        public TrackEngine(string songName)
        {
            this.songName = songName;
            runCountdown = true;
            running = false;
            trackBall = new TrackBall();
            map = new Map();
            performanceTracker = new PerformanceTracker();
            comboHitInfoLabels = new LabelListOverflow(new Vector2(Globals.WindowWidth / 2 - 30, Globals.WindowHeight / 2), 2000);
            line1 = new Step() { Rectangle = new Rectangle(Globals.WindowWidth / 4, 0, 1, Globals.WindowHeight) };
            line2 = new Step() { Rectangle = new Rectangle(Globals.WindowWidth / 2, 0, 1, Globals.WindowHeight) };
            line3 = new Step() { Rectangle = new Rectangle(Globals.WindowWidth / 2 + Globals.WindowWidth / 4, 0, 1, Globals.WindowHeight) };

            scoreLabel = new Label("Score: 0", new Vector2(0, 0));
            accuracyLabel = new Label("Acc: 0%", new Vector2(0, 30));
            timerLabel = new Label("0", new Vector2(Globals.WindowWidth / 2 - 15, Globals.WindowHeight / 2 - 15));
            endGameLabel = new Label("You  failed", new Vector2(Globals.WindowWidth / 2 - 15, Globals.WindowHeight / 2 - 15))
            {
                Visible = false
            };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            trackBall.Draw(spriteBatch);
            scoreLabel.Draw(spriteBatch);
            accuracyLabel.Draw(spriteBatch);
            endGameLabel.Draw(spriteBatch);
            comboHitInfoLabels.Draw(spriteBatch);
            //line1.Draw(spriteBatch);
            //line2.Draw(spriteBatch);
            //line3.Draw(spriteBatch);

            if (runCountdown)
            {
                timerLabel.Draw(spriteBatch);
            }
        }

        private void HandleActionKeysPressed()
        {
            if (trackBall.Rectangle.Intersects(map.CurrentStep.Rectangle))
            {
                performanceTracker.AddHitAccuracy(trackBall.Rectangle.Width, trackBall.CenterX, map.CurrentStep.CenterX);
                score += performanceTracker.GetLastHitScore();

                accuracyLabel.Text = performanceTracker.Accuracy;
                map.NextStep();

                SoundPlayer.PlayEffect(SoundPlayer.SoundEffects.StepHit);
                trackBall.ChangeDirection();
                comboHitInfoLabels.AddLabel(performanceTracker.GetLastHitInfoLabel());
            }
            else
            {
                TrackFailed();
            }
        }

        private void UpdateCountDown(GameTime gameTime)
        {
            if (timeToStart > 0)
            {
                elapsedTimeToStart += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedTimeToStart > 1000)
                {
                    timeToStart -= 1;
                    elapsedTimeToStart = 0;
                }
            }
            if (timeToStart <= 0)
            {
                runCountdown = false;
                StartTrack();
            }
        }

        private void StartTrack()
        {
            trackBall.Start();
            SoundPlayer.PlaySong(songName);
        }

        //Starts Game and CountdownTimer
        public void InitGame()
        {
            score = 0;
            elapsedTimeToStart = 0;
            timeToStart = 3;
            running = true;
            runCountdown = true;
            endGameLabel.Visible = false;
            accuracyLabel.Text = "Acc: 0%";
            map.Reset();
            performanceTracker.Reset();
            comboHitInfoLabels.Reset();
        }

        private void TrackFailed()
        {
            endGameLabel.Visible = true;
            running = false;
            comboHitInfoLabels.Reset();

            SoundPlayer.StopSong();
            SoundPlayer.PlayEffect(SoundPlayer.SoundEffects.TrackFailed);
        }

        public void Update(GameTime gameTime)
        {
            if (!running)
            {
                return;
            }

            if (runCountdown) 
            {
                UpdateCountDown(gameTime);
                return;
            }
          
            trackBall.Update(gameTime);
            comboHitInfoLabels.Update(gameTime);

            if (trackBall.CenterX < 100 || trackBall.CenterX > Globals.WindowWidth - 100)
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
