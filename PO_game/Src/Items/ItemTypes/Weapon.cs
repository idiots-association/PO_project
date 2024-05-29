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
    public abstract class Weapon: Item{
        public Tuple <int,int> Damage { get; set; }
        public Weapon(Texture2D texture, string name, string description, string rarity, Tuple <int,int> damage, Player player): base(texture, name, description, rarity,player){
            Damage = damage;
        }
        public abstract void Attack(Player player);

        public override void Draw(SpriteBatch spriteBatch, Vector2 position){
            spriteBatch.Draw(Texture, position, Color.White);
        }
    }
}