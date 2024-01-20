using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RythmGame.Utils
{
    public static class InputHandler
    {
        private static MouseState currentMouseState;
        private static MouseState lastMouseState;
        private static KeyboardState currentKeyboardSate;
        private static KeyboardState lastKeyboardState;

        public static void Update()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            lastKeyboardState = currentKeyboardSate;
            currentKeyboardSate = Keyboard.GetState();
        }

        public static bool IsKeyPressed(Keys key)
        {
            return currentKeyboardSate.IsKeyDown(key) && !lastKeyboardState.IsKeyDown(key);
        }

        public static bool LeftClick()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed &&
                   lastMouseState.LeftButton == ButtonState.Released;
        }

        public static bool RightClick()
        {
            return currentMouseState.RightButton == ButtonState.Pressed &&
                   lastMouseState.RightButton == ButtonState.Released;
        }
    }
}
