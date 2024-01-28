﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.UiComponents;
using RythmGame.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RythmGame.GamePlay.Track
{
    public class PauseGameUI
    {
        private Label pauseLabel;

        public PauseGameUI()
        {
            pauseLabel = new Label("Game Paused");
            pauseLabel.Position = new Vector2(Globals.WindowWidth / 2 - pauseLabel.Size.X / 2, 150);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            pauseLabel.Draw(spriteBatch);
        }

    }
}
