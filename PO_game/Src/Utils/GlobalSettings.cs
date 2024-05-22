using Microsoft.Xna.Framework;

namespace PO_game.Src.Utils
{
    public static class GlobalSettings
    {
        public static int TileSize { get; set; } = 32;
        public static double MoveSpeed { get; set; } = 0.15;
        public static Vector2 MoveDirection { get; set; } = Vector2.Zero;
    }
}
