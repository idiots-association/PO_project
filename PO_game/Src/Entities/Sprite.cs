using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PO_game.Src.Entities
{
    /// <summary>
    /// <c>Sprite</c> is a class that represents a sprite in the game.
    /// <para>It allows the creation of sprites with custom textures and positions.</para>
    /// </summary>
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }

        public Sprite(Texture2D texture2D)
        {
            Texture = texture2D;
        }
    }

}
