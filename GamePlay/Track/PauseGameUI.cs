using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RythmGame.UiComponents;
using RythmGame.Utils;

namespace RythmGame.GamePlay.Track
{
    public class PauseGameUI
    {
        private Label pauseLabel;

        public PauseGameUI()
        {
            pauseLabel = new Label("Game Paused") { LabelColor = Color.WhiteSmoke };
            pauseLabel.Position = new Vector2(Globals.WindowWidth / 2 - pauseLabel.Size.X / 2, 150);
        }

        public void Draw()
        {
            pauseLabel.Draw();
        }

    }
}
