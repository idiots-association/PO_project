using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
using Microsoft.Xna.Framework.Content;
using PO_game.Src.Effects;
using PO_game.Src.Screens;

namespace PO_game.Src.Items
{
    /// <summary>
    /// <c>Shield</c> is a class that represents a Shield item in the game.
    /// <para>It allows the creation of a Shield item with blocking and other effects.</para>
    /// </summary>
    public class Shield : OffHand
    {
        public Shield(Texture2D texture, string name, string description, ItemRarity rarity, int block) : base(texture, name, description, rarity, block){}

        public override void Use(BattleScreen battleScreen)
        {
            battleScreen.player.damageReduction += block;
        }
    }
}