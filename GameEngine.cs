using Microsoft.Xna.Framework;
using RythmGame.Utils;
using Microsoft.Xna.Framework.Input;
using RythmGame.GameObjects;
using RythmGame.UiComponents;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RythmGame
{
    public class GameEngine
    {
        private int _score;
        private int score
        {
            get { return _score; }
            set { 
                _score = value;
                scoreLabel.Text = _score.ToString();
            }
        }
        private int _timeToStart;
        private int timeToStart {
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

        public GameEngine()
        {
            running = false;
            trackBall = new TrackBall();
            map = new Map();
            scoreLabel = new Label("0", new Vector2(0, 0));
            timerLabel = new Label("0", new Vector2(Configuration.WindowWidth / 2 - 15, Configuration.WindowHeight / 2 - 15));
            endGameLabel = new Label("You  failed", new Vector2(Configuration.WindowWidth / 2 - 15, Configuration.WindowHeight / 2 - 15))
            {
                Visible = false
            };
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(timeToStart > 0)
            {
                timerLabel.Draw(spriteBatch);
            }
            trackBall.Draw(spriteBatch);
            map.Draw(spriteBatch);
            scoreLabel.Draw(spriteBatch);
            endGameLabel.Draw(spriteBatch);
        }

        public void LoadContent()
        {
            trackBall.LoadContent();
            scoreLabel.LoadContent();
            endGameLabel.LoadContent();
            timerLabel.LoadContent();
        }

        public void StartGame()
        {
            score = 0;
            elapsedTimeToStart = 0;
            timeToStart = 3;
            running = true;
            endGameLabel.Visible = false;
            trackBall.Initialize();
            map.Initialize();
        }

        private void GameFailed()
        {
            endGameLabel.Visible = true;
            running = false;

            AssetManager.GetSoundEffect("rocketExplosionSound").Play(0.05f, 0.0f, 0.0f);
        }

        public void Update(GameTime gameTime)
        {
            if(timeToStart > 0)
            {
                elapsedTimeToStart += gameTime.ElapsedGameTime.TotalMilliseconds;
                if(elapsedTimeToStart> 1000)
                {
                    timeToStart -= 1;
                    elapsedTimeToStart = 0;
                }
                return;
            }

            if(!running)
            { 
                return;
            }

            trackBall.Update(gameTime);

            if (trackBall.Rectangle.X < 100 || trackBall.Rectangle.X > Configuration.WindowWidth - 100)
            {
                GameFailed();
            }

            if (InputHandler.IsKeyPressed(Keys.Space))
            {
                if (trackBall.Rectangle.Intersects(map.CurrentStep.Rectangle))
                {
                    score++;
                    map.NextStep();

                    AssetManager.GetSoundEffect("slimeSplash").Play(0.05f, 0.0f, 0.0f);
                    trackBall.ChangeDirection();
                }
                else
                {
                    GameFailed();
                }
            }
        }
    }
}
