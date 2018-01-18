using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECSAttempt {
    public static class KeyInput {
        private static KeyboardState newKeyPressState;
        private static KeyboardState oldKeyPressState;
        internal static void UpdateKeyState() {
            oldKeyPressState = newKeyPressState;
        }

        public static bool KeyPress(Keys key) {
            newKeyPressState = Keyboard.GetState();
            if (newKeyPressState.IsKeyDown(key) && oldKeyPressState.IsKeyUp(key))
                return true;
            else
                return false;
        }

        public static bool KeyHold(Keys key) {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(key))
                return true;
            else
                return false;
        }
    }
}