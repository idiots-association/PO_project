using Microsoft.Xna.Framework;

namespace PO_game.Src
{
    public static class GlobalSettings
    {
        public static int TileSize { get; set; } = 32;
        public static int MoveSpeed { get; set; } = 2;
        public static Vector2 MoveDirection { get; set; } = Vector2.Zero;
    }
}
