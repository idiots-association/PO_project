using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
using PO_game.Src.Screens;

namespace PO_game.Src.Items
{
    /// <summary>
    /// <c>OffHand</c> is a class that represents a Off-Hand item in the game.
    /// <para>It allows the creation of a Off-Hand item with custom effects in combat.</para>
    /// </summary>
    public abstract class OffHand : Item
    {
        public int block {get; set;}
        public OffHand(Texture2D texture, string name, string description, ItemRarity rarity, int block) : base(texture, name, description, rarity)
        {
            this.block = block;
        }
        public abstract void Use(BattleScreen battleScreen);
        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, Color.White);
        }
    }
}