using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RythmGame.GameObjects;
using RythmGame.Utils;
using RythmGame.UiComponents;

namespace RythmGame.GamePlay
{
    public class TrackEngine
    {
        private int _score;
        private int score
        {
            get { return _score; }
            set
            {
                _score = value;
                scoreLabel.Text = _score.ToString();
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
        private Label endGameLabel;
        private Label timerLabel;
        private double elapsedTimeToStart;

        public TrackEngine(string songName)
        {
            runCountdown = true;
            running = false;
            trackBall = new TrackBall();
            map = new Map(songName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            trackBall.Draw(spriteBatch);
            scoreLabel.Draw(spriteBatch);
            endGameLabel.Draw(spriteBatch);

            if (runCountdown)
            {
                timerLabel.Draw(spriteBatch);
            }
        }

        public void Initialize()
        {
            scoreLabel = new Label("0", new Vector2(0, 0));
            timerLabel = new Label("0", new Vector2(UserPrefs.Settings.WindowWidth / 2 - 15, UserPrefs.Settings.WindowHeight / 2 - 15));
            endGameLabel = new Label("You  failed", new Vector2(UserPrefs.Settings.WindowWidth / 2 - 15, UserPrefs.Settings.WindowHeight / 2 - 15))
            {
                Visible = false
            };
        }

        public void LoadContent()
        {
            trackBall.LoadContent();
            scoreLabel.LoadContent();
            endGameLabel.LoadContent();
            timerLabel.LoadContent();
        }

        private void HandleActionKeysPressed()
        {
            if (trackBall.Rectangle.Intersects(map.CurrentStep.Rectangle))
            {
                score++;
                map.NextStep();

                SoundPlayer.PlayEffect(SoundPlayer.SoundEffects.StepHit);
                trackBall.ChangeDirection();
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
            map.StartMap();
            trackBall.Start();
        }

        public void StartGame()
        {
            score = 0;
            elapsedTimeToStart = 0;
            timeToStart = 3;
            running = true;
            runCountdown = true;
            endGameLabel.Visible = false;
            trackBall.Initialize();
            map.Initialize();
        }

        private void TrackFailed()
        {
            endGameLabel.Visible = true;
            running = false;
            map.StopMap();

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

            if (trackBall.Rectangle.X < 100 || trackBall.Rectangle.X > UserPrefs.Settings.WindowWidth - 100)
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
