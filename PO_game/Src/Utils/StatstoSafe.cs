using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PO_game.Src.Utils;

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

public class StatsToSave
{
    public Vector2Data Position { get; set; }
    public string Name { get; set; }
}