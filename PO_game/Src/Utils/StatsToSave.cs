using PO_game.Src.Maps;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PO_game.Src.Utils;

/// <summary>
///<c>StatsToSave</c> is a class that represents the stats of the player that need to be saved.
/// <para> For now, it contains the position, name and current map of the player.
/// </para>
/// </summary>

/// <summary>
/// <c>Vector2Data</c> is a class that represents a Vector2 object in a serializable way.
/// </summary>

public class Vector2Data
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vector2Data() { }

    public Vector2Data(Vector2 vector)
    {
        X = vector.X;
        Y = vector.Y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(X, Y);
    }
}

/// <summary>
/// <c>StatsToSave</c> is a class that represents the stats of the player that need to be saved.
/// </summary>
public class StatsToSave
{
    /// <summary>
    /// Position of the player.
    /// </summary>
    public Vector2Data Position { get; set; }
    /// <summary>
    /// Name of the player.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Map the player is currently on.
    /// </summary>
    public MapId CurrentMapId { get; set; }
}