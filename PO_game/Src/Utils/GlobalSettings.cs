using Microsoft.Xna.Framework;

namespace PO_game.Src.Utils
{
    public static class GlobalSettings
    {
        public static int ScreenWidth { get; set; } = 800;
        public static int ScreenHeight { get; set; } = 480;
        public static int TileSize { get; set; } = 16;
        public static double MoveSpeed { get; set; } = 0.15;
        public static float Scale { get; set; } = 2f;

        public static bool ShowCollisions { get; set; } = true;
    }
    public enum CharacterState
    {
        MovingUp,
        MovingDown,
        MovingLeft,
        MovingRight,
        Idle
    }

    public enum Collision
    {
        NoColission = -1,
        NoPassCollision = 0,
        WarpCollision = 1,
        PlayerCollision = 2,
    }
}
