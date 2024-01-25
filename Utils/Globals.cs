
namespace RythmGame.Utils
{
    public static class Globals
    {
        public enum GAME_STATE {
            MAIN_MENU = 0,
            PLAYING = 1,
            SELECTION_SCREEN = 2
        }

        public static int WindowWidth = 1024;
        public static int WindowHeight = 600;

        public static GAME_STATE GameState = 0;

    }
}
