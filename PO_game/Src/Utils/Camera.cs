using Microsoft.Xna.Framework;
using PO_game.Src.Entities;

namespace PO_game.Src.Utils
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Player player)
        {
            var position = Matrix.CreateTranslation(
                               -player.Sprite.Position.X - player.Sprite.Texture.Width / 2,
                               -player.Sprite.Position.Y - player.Sprite.Texture.Height / 2,
                               0);

            var offset = Matrix.CreateTranslation(
                               Globals.ScreenWidth / 2,
                               Globals.ScreenHeight / 2,
                               0);

            Transform = position * offset;
        }
    }
}
