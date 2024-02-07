using RythmGame.Utils;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RythmGame.MapEditor
{
    public class MapEditorScreen
    {
        private Texture2D lineTexture;
        private TrackBall trackBall;
        private Map map;
        private int gridSnap;
        private bool tryRun;
        private Rectangle[] grid;

        public MapEditorScreen()
        {
            lineTexture = AssetManager.GetTexture("stepLine");
            trackBall = new TrackBall(3);
            map = new Map();
            tryRun = false;
            gridSnap = 48;
        }

        public void Draw()
        {
            Globals.GraphicsDevice.Clear(Color.LightSeaGreen);
            for (int i = Globals.TrackBallStartX + Globals.TrackBallWidth / 2; i < Globals.WindowWidth; i += gridSnap)
            {
                Globals.SpriteBatch.Draw(lineTexture, new Rectangle(i, 0, 1, Globals.WindowHeight), Color.Black);
            }
            for (int i = Globals.TrackBallStartX + Globals.TrackBallWidth / 2; i > 0; i -= gridSnap)
            {
                Globals.SpriteBatch.Draw(lineTexture, new Rectangle(i, 0, 1, Globals.WindowHeight), Color.Black);
            }
            for (int i = Globals.TrackBallHeight / 2; i < Globals.WindowHeight; i += 48)
            {
                Globals.SpriteBatch.Draw(lineTexture, new Rectangle(0, i, Globals.WindowWidth, 1), Color.Black);
            }

            Globals.SpriteBatch.Draw(lineTexture, new Rectangle(InputHandler.CurrentMouseState.X, map.NextStepPosY + Globals.StepHeight * map.StepIndex, 3, 48), Color.DarkOrange);
            map.Draw();
            trackBall.Draw();
        }

        public void Initialize()
        {
        }

        public void Update()
        {
            if (tryRun)
            {
                trackBall.Update(map);
            }

            if (InputHandler.IsKeyPressed(Keys.OemOpenBrackets))
            {
                if(gridSnap > 6)
                {                    
                    gridSnap /= 2;
                }
            }
            if (InputHandler.IsKeyPressed(Keys.OemCloseBrackets))
            {
                if (gridSnap < 256)
                {
                    gridSnap *= 2;
                }
            }

            if (InputHandler.IsKeyPressed(Keys.W))
            {
                map.NextStep();
            }
            if (InputHandler.IsKeyPressed(Keys.S))
            {
                map.PreviousStep();
            }

            if (InputHandler.LeftClick())
            {
                map.AddStep(InputHandler.CurrentMouseState.X);
            }
        }
    }
}
