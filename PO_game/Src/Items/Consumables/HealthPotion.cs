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
        public int Health { get; set; }
        public HealthPotion(Texture2D texture, string name, string description, string rarity, int health,int quantity,Player player): base(texture, name, description, rarity,quantity,player){
            Health = health;
        }
        public override void Use(){
            if (_playerBinding.Health + Health > _playerBinding.MaxHealth)
                _playerBinding.Health = _playerBinding.MaxHealth;
            else
                _playerBinding.Health += Health;
            Quantity--;
        }
    }
}