using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using PO_game.Src;
using PO_game.Src.Utils;
using PO_game.Src.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace PO_game.Src.Items{
    public class Weapon: Item{
        public int minDamage { get; set; }
        public int maxDamage { get; set; }
        public Weapon(Texture2D texture, string name, string description, string rarity, int mindamage, int maxdamage, Player player): base(texture, name, description, rarity,player){
            minDamage = mindamage;
            maxDamage = maxdamage;
        }
        //public abstract void Attack(Player player);

        public override void Draw(SpriteBatch spriteBatch, Vector2 position){
            spriteBatch.Draw(Texture, position, Color.White);
        }
    }
}