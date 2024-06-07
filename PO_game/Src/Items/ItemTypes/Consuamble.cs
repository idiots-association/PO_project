using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
namespace PO_game.Src.Items
{
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