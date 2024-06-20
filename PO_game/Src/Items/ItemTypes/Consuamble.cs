using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
namespace PO_game.Src.Items
{
    /// <summary>
    /// <c>Consumable</c> is an abstract class that represents a consumable item in the game.
    /// <para>It allows the creation of consumable items with custom textures, names, descriptions, rarities and quantities.</para>
    /// </summary>
    public abstract class Consumable : Item
    {
        public int Quantity { get; set; }
        public Consumable(Texture2D texture, string name, string description, string rarity, int quantity) : base(texture, name, description, rarity)
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