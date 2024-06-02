using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Utils;

namespace PO_game.Src
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float OffsetX { get; set;}
        public float OffsetY { get; set; }

        public Sprite(Texture2D texture2D, Vector2 position)
        {
            Texture = texture2D;
            Position = position;
            OffsetX = (GlobalSettings.TileSize / 2f) - (Texture.Width / 2f);
            OffsetY = (GlobalSettings.TileSize / 2f) - (Texture.Height / 2f);
        }
    }

}
