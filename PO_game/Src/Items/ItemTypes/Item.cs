using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;

namespace PO_game.Src.Items
{
    /// <summary>
    /// Represents the possible item rarities in the game.
    /// </summary>
    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Unique
    }
    /// <summary>
    /// <c>Item</c> is an abstract class that represents an item in the game.
    /// <para>It allows the creation of items with custom textures, names, descriptions and rarities.</para>
    /// </summary>
    public abstract class Item
    {
        public Texture2D Texture;
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemRarity Rarity { get; set; }
        
        
        public Item(Texture2D texture, string name, string description, ItemRarity rarity)
        {
            Texture = texture;
            Name = name;
            Description = description;
            Rarity = rarity;
        }
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 position);
    }
}