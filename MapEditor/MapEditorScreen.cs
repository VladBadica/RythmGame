using RythmGame.Utils;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace RythmGame.MapEditor
{
    public class MapEditorScreen
    {
        private Texture2D lineTexture;
        private TrackBall trackBall;
        private Map map;
        private int gridSnap;
        private bool tryRun;
        private int MouseSnapPosX;

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

            map.Draw();
            trackBall.Draw();
            Globals.SpriteBatch.Draw(lineTexture, new Rectangle(MouseSnapPosX, map.NextStepPosY + Globals.StepHeight * map.StepIndex, 3, 48), map.AllowNextStepPosX(MouseSnapPosX) ? Color.DarkOrange: Color.Red);
        }

        private int SnapMouseToGrid()
        {
            if(InputHandler.CurrentMouseState.X >= Globals.TrackBallStartX + Globals.TrackBallWidth / 2)
            {
                for (int i = Globals.TrackBallStartX + Globals.TrackBallWidth / 2; i < Globals.WindowWidth; i += gridSnap)
                {
                    if (Math.Abs(InputHandler.CurrentMouseState.X - i) <= gridSnap / 2)
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (int i = Globals.TrackBallStartX + Globals.TrackBallWidth / 2; i > 0; i -= gridSnap)
                {
                    if (Math.Abs(InputHandler.CurrentMouseState.X - i) <= gridSnap / 2)
                    {
                        return i;
                    }
                }
            }

            return InputHandler.CurrentMouseState.X;
            
        }

        public void Initialize()
        {
            tryRun = false;
            gridSnap = 48;
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
            MouseSnapPosX = SnapMouseToGrid();
            if (InputHandler.LeftClick())
            {
                if(MouseSnapPosX > 0 && MouseSnapPosX < Globals.WindowWidth && InputHandler.CurrentMouseState.Y > 0 && InputHandler.CurrentMouseState.Y < Globals.WindowHeight)
                {
                    map.AddStep(MouseSnapPosX);
                }
            }
        }
    }
}
