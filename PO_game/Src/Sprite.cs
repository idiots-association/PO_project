using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PO_game.Src
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Color[] SpriteColor { get; set; }

        public Sprite(Texture2D texture2D, Color[] color, Vector2 position)
        {
            Texture = texture2D;
            SpriteColor = color;
            Position = position;
        }
    }

}
