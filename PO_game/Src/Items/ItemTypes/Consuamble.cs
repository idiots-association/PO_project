using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
using Microsoft.Xna.Framework.Content;
using PO_game.Src.Items.Consumables;

namespace PO_game.Src.Items
{
    /// <summary>
    /// Defines the types of consumable items that can be created.
    /// </summary>
    public enum PotionType
    {
        HealthPotion,
        ManaPotion
    }

    /// <summary>
    /// Class <c>PotionFactory</c> is a class that creates consumable items based on the type of potion requested.
    /// It stores all the pre-set potion types and their respective textures, names, descriptions, rarities and health restored.
    /// </summary>
    public static class PotionFactory
    {
        /// <summary>
        /// Creates a potion based on the type of potion requested.
        /// </summary>
        /// <param name="potionType">The type of potion to be created.</param>
        /// <param name="itemRarity">The rarity of the potion to be created.</param>
        /// <param name="quantity">The quantity of the potion to be created.</param>
        /// <param name="content">The content manager used to load the textures of the potions.</param>
        /// <returns></returns>
        public static Consumable CreatePotion(PotionType potionType, ItemRarity itemRarity,int quantity, ContentManager content)
        {
            switch (potionType)
            {
                case PotionType.HealthPotion:
                    switch (itemRarity)
                    {
                        case ItemRarity.Common:
                            return new HealthPotion(content.Load<Texture2D>("Items/medium_health_potion"), "Basic Health Potion", "A potion that restores 20 health points.", ItemRarity.Common, 20, quantity);
                        default:
                            return new HealthPotion(content.Load<Texture2D>("Items/medium_health_potion"), "Medium Health Potion", "A potion that restores 45 health points.", ItemRarity.Uncommon, 20, quantity);
                    }
                case PotionType.ManaPotion:
                    return null;
                default:
                    return null;
            }
        }
    }
    /// <summary>
    /// <c>Consumable</c> is an abstract class that represents a consumable item in the game.
    /// <para>It allows the creation of consumable items with custom textures, names, descriptions, rarities and quantities.</para>
    /// </summary>
    public abstract class Consumable : Item
    {
        public int Quantity { get; set; }
        public Consumable(Texture2D texture, string name, string description, ItemRarity rarity, int quantity) : base(texture, name, description, rarity)
        {
            Quantity = quantity;
        }
        public abstract void Use(Character character);
        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, Color.White);
        }

    }
}