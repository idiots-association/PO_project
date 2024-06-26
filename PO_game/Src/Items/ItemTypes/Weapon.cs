using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
using System;
namespace PO_game.Src.Items
{
    /// <summary>
    /// <c>Weapon</c> is a class that represents a weapon in the game.
    /// <para>It allows the creation of weapons with custom damage values.</para>
    /// </summary>
    
    public class Weapon : Item
    {
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public Weapon(Texture2D texture, string name, string description, ItemRarity rarity, int mindamage, int maxdamage) : base(texture, name, description, rarity)
        {
            MinDamage = mindamage;
            MaxDamage = maxdamage;
            
            
        }
        public int Attack()
        {
            Random rand = new Random();
            int damage = rand.Next(MinDamage, MaxDamage);
            return damage;
        }


        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, Color.White);
            
        }
    }
}