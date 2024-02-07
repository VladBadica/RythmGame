using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RythmGame.Utils
{
    public static class Globals
    {
        public enum GAME_STATE {
            MAIN_MENU = 0,
            PLAYING = 1,
            SELECTION_SCREEN = 2,
            MAP_EDITOR = 3
        }

        public static int WindowWidth = 1024;
        public static int WindowHeight = 600;

        public static int StepHeight = 48;
        public static int StepWidth = 3;

        public static int TrackBallWidth = 48;
        public static int TrackBallHeight = 48;
        public static int TrackBallStartX = WindowWidth / 2 - TrackBallWidth / 2;
        public static int TrackBallStartY = WindowHeight - TrackBallHeight - 48;

        public static GAME_STATE GameState;

        public static SpriteBatch SpriteBatch;
        public static GameTime GameTime;
        public static GraphicsDevice GraphicsDevice;

    }
}
