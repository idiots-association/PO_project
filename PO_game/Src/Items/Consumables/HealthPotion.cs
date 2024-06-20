using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
namespace PO_game.Src.Items.Consumables
{
    /// <summary>
    /// <c>HealthPotion</c> is a class that represents a health potion in the game.
    /// <para>It allows the creation of health potions with custom health values.</para>
    /// </summary>
    public class HealthPotion : Consumable
    {
        public int health { get; set; }
        public HealthPotion(Texture2D texture, string name, string description, string rarity, int health, int quantity) : base(texture, name, description, rarity, quantity)
        {
            this.health = health;
        }
        public override void Use(Character character)
        {
            if (character.health + health > character.maxHealth)
                character.health = character.maxHealth;
            else
                character.health += health;
            Quantity--;
        }
    }
}