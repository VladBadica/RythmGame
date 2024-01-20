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
        private int score;
        private int failCount;
        private bool running;
        private TrackBall trackBall;
        private Map map;
        private Label scoreLabel;
        private Label endGameLabel;

        public GameEngine()
        {
            trackBall = new TrackBall();
            map = new Map();
            scoreLabel = new Label(score.ToString(), new Vector2(0, 0));
            endGameLabel = new Label("You  failed", new Vector2(Configuration.WindowWidth / 2 - 15, Configuration.WindowHeight / 2 - 15))
            {
                Visible = false
            };
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            trackBall.Draw(spriteBatch);
            map.Draw(spriteBatch);
            scoreLabel.Draw(spriteBatch);
            endGameLabel.Draw(spriteBatch);
        }

        public void Initialize()
        {
            score = 0;
            failCount = 0;
            trackBall.Initialize();
            map.Initialize();
            running = true;
        }

        public void LoadContent()
        {
            trackBall.LoadContent();
            scoreLabel.LoadContent();
            endGameLabel.LoadContent();
        }

        public void StartGame()
        {
            running = true;
        }

        private void GameFailed()
        {
            endGameLabel.Visible = true;
            running = false;
        }

        public void Update(GameTime gameTime)
        {
            if(!running)
            { 
                return;
            }

            trackBall.Update(gameTime);
            if (InputHandler.IsKeyPressed(Keys.Space))
            {
                if (trackBall.Rectangle.Intersects(map.CurrentStep.Rectangle))
                {
                    score++;
                    scoreLabel.Text = score.ToString();
                    map.NextStep();
                }
                else
                {
                    failCount++;
                }
            }

            if(failCount == 3)
            {
                GameFailed();
            }
        }
    }
}
