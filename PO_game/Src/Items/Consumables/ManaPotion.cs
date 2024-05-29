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
    public class ManaPotion: Consumable{
        public int Mana { get; set; }
        public ManaPotion(Texture2D texture, string name, string description, string rarity, int mana,int quantity,Player player): base(texture, name, description, rarity,quantity,player){
            Mana = mana;
        }
        public override void Use(){
            if (_playerBinding.Mana + Mana > _playerBinding.MaxMana)
                _playerBinding.Mana = _playerBinding.MaxMana;
            else
                _playerBinding.Mana += Mana;
            Quantity--;
        }
    }
}