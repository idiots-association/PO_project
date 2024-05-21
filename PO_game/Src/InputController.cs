using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PO_game.Src
{
    public class InputController
    {
        public Vector2 GetMoveDirection()
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                return new Vector2(0, -1);
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                return new Vector2(0, 1);
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                return new Vector2(-1, 0);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                return new Vector2(1, 0);
            }
            else
            {
                return Vector2.Zero;
            }
        }

    }
}
