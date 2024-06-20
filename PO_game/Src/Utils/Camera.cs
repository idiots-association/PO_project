using Microsoft.Xna.Framework;
using PO_game.Src.Entities;

namespace PO_game.Src.Utils
{
    /// <summary>
    /// <c>Camera</c> class to follow the player.
    /// </summary>
    public class Camera
    {
        public Matrix Transform { get; private set; }

        /// <summary>
        /// <c>Camera</c> method to follow the player.
        /// <para>
        /// Follow takes <paramref name="player"/> as a parameter and by using the <paramref name="=player.Sprite.Position"/>, it sets the camera's position to follow the player, 
        /// keeping the player in the center of the screen. <see cref="Sprite"/>
        /// </para>
        /// </summary>
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
