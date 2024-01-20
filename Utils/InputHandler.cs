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
        public static MouseState CurrentMouseState;
        public static MouseState LastMouseState;
        public static KeyboardState CurrentKeyboardState;
        public static KeyboardState LastKeyboardState;

        public static void Update()
        {
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
            LastKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }

        public static bool IsKeyPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && !LastKeyboardState.IsKeyDown(key);
        }

        public static bool LeftClick()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed &&
                   LastMouseState.LeftButton == ButtonState.Released;
        }

        public static bool RightClick()
        {
            return CurrentMouseState.RightButton == ButtonState.Pressed &&
                   LastMouseState.RightButton == ButtonState.Released;
        }
    }
}
