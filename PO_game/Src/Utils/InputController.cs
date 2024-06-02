using Microsoft.Xna.Framework.Input;

namespace PO_game.Src.Utils
{
    public class InputController
    {
        private KeyboardState _oldKeyboardState, _currentKeyboardState;

        public InputController(KeyboardState oldKeyboardState, KeyboardState currentKeyboardState)
        {
            _oldKeyboardState = oldKeyboardState;
            _currentKeyboardState = currentKeyboardState;
        }

        public void Update()
        {
            _oldKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
        }

        public InputController()
        {
        }

        public bool IsKeyDown(Keys key) { return _currentKeyboardState.IsKeyDown(key); }
        public bool IsKeyUp(Keys key) { return _currentKeyboardState.IsKeyUp(key); }
        public bool isKeyPressed(Keys key) { return _oldKeyboardState.IsKeyUp(key) && _currentKeyboardState.IsKeyDown(key); }
    }
}
