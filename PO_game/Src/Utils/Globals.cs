using Microsoft.Xna.Framework.Graphics;

namespace PO_game.Src.Utils
{
    /// <summary>
    /// Contains global settings and properties used throughout the game.
    /// </summary>
    public static class Globals
    {

        public const int ScreenWidth = 800;
        public const int ScreenHeight = 480;
        public const int TileSize = 16;
        public const int InvSlotSize = 64;
        public static double MoveSpeed { get; set; } = 0.15;
        public const float Scale  = 2.5f;
        public static SpriteFont gameFont { get; set; }
        public static bool ShowCollisions { get; set; } = true;
    }

    /// <summary>
    /// Defines the possible states of a character.
    /// </summary>
    public enum CharacterState
    {
        MovingUp,
        MovingDown,
        MovingLeft,
        MovingRight,
        Idle
    }


    /// <summary>
    /// Defines the types of collision interactions.
    /// </summary>
    /// <list type="bullet">
    /// <item>
    /// <description><c>NoCollision</c>: Represents no collision. This value is used to indicate that no collision has occurred.</description>
    /// </item>
    /// <item>
    /// <description><c>NoPassCollision</c>: Represents a collision where the character cannot pass through the collided object.</description>
    /// </item>
    /// <item>
    /// <description><c>WarpCollision</c>: Represents a collision that triggers a warp or teleportation effect, moving the character to a different location.</description>
    /// </item>
    /// <item>
    /// <description><c>PlayerCollision</c>: Represents a collision with another player character. This can be used to trigger interactions between player characters.</description>
    /// </item>
    /// </list>
    public enum Collision
    {
        NoCollision = -1,
        NoPassCollision = 0,
        WarpCollision = 1,
        PlayerCollision = 2,
    }

}
