using System;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
namespace PO_game.Src.Items.Consumables
{
    /// <summary>
    /// <c>ManaPotion</c> is a class that represents a mana potion in the game.
    /// <para>It allows the creation of mana potions with custom mana values.</para>
    /// </summary>
    public class ManaPotion : Consumable
    {
        public int mana { get; set; }
        public ManaPotion(Texture2D texture, string name, string description, string rarity, int mana, int quantity) : base(texture, name, description, rarity, quantity)
        {
            this.mana = mana;
        }
        public override void Use(Character character)
        {
            if (character.mana + mana > character.maxMana)
                character.mana = character.maxMana;
            else
                character.mana += mana;
            Quantity--;
        }
    }
}