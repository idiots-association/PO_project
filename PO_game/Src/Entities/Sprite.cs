using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PO_game.Src.Entities
{
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
