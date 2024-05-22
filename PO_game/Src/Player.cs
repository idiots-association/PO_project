using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PO_game.Src
{
    public class Player : Character
    {
        public Player(Sprite sprite) : base(sprite)
        {
        }

        public override void Update(GameTime gameTime, InputController inputController)
        {
            var moveDirection = inputController.GetMoveDirection();
            Position += moveDirection;
        }

    }
}
