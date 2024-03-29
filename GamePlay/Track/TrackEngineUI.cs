﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.UiComponents;
using RythmGame.Utils;

namespace RythmGame.GamePlay.Track
{
    public class TrackEngineUI
    {
        private Label scoreLabel;
        private Label accuracyLabel;
        private Label timerLabel;
        public LabelListOverflow ComboHitInfoLabels;

        public TrackEngineUI()
        {
            ComboHitInfoLabels = new LabelListOverflow(new Vector2(Globals.WindowWidth / 2 - 30, Globals.WindowHeight / 2), 2000) { LabelsColor = Color.Yellow };
            scoreLabel = new Label("Score: 0") { LabelColor = Color.WhiteSmoke };
            scoreLabel.Position = new Vector2(0, 0);
            accuracyLabel = new Label("Acc: 0%") { LabelColor = Color.WhiteSmoke };
            accuracyLabel.Position = new Vector2(0, 30);
            timerLabel = new Label("0") { LabelColor = Color.WhiteSmoke };
            timerLabel.Position = new Vector2(Globals.WindowWidth / 2 - timerLabel.Size.X / 2, Globals.WindowHeight / 2 - timerLabel.Size.Y / 2 - 50);

        }

        public void Draw(TrackEngine trackEngine)
        {

            scoreLabel.Text = "Score: " + trackEngine.Score.ToString();
            accuracyLabel.Text = "Acc: " + trackEngine.PerformanceTracker.Accuracy + "%";
            
            scoreLabel.Draw();
            accuracyLabel.Draw();
            ComboHitInfoLabels.Draw();

            if (trackEngine.ShowCountdown)
            {
                timerLabel.Text = trackEngine.TimeToStart.ToString();
                timerLabel.Draw();
            }
        }

        public void Reset()
        {
            ComboHitInfoLabels.Reset();
        }

        public void Update(TrackEngine trackEngine)
        {
            ComboHitInfoLabels.Update();
        }

    }
}
