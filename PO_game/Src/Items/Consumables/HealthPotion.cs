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
namespace PO_game.Src.Items.Consumables{
    public class HealthPotion: Consumable{
        public int health { get; set; }
        public HealthPotion(Texture2D texture, string name, string description, string rarity, int health,int quantity,Character character): base(texture, name, description, rarity,quantity, character){
            this.health = health;
        }
        public override void Use(){
            if (_characterBinding.health + health > _characterBinding.maxHealth)
                _characterBinding.health = _characterBinding.maxHealth;
            else
                _characterBinding.health += health;
            Quantity--;
        }
    }
}