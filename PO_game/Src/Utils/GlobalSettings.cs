using Microsoft.Xna.Framework;

namespace PO_game.Src.Utils
{
    public static class GlobalSettings
    {
        public static int ScreenWidth { get; set; } = 800;
        public static int ScreenHeight { get; set; } = 480;
        public static int TileSize { get; set; } = 32;
        public static double MoveSpeed { get; set; } = 0.15;
    }
}
