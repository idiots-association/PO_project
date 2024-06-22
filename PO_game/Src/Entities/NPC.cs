using System.Numerics;

namespace PO_game.Src.Entities
{
    /// <summary>
    /// <c>NPC</c> is a class that represents a non-playable friendly character in the game.
    /// <para>Doesn't do much for now.</para>
    /// </summary>
    public class NPC : Character
    {
        public NPC(Sprite sprite, Vector2 tilePosition) : base(sprite, tilePosition)
        {
        }
    }
}
