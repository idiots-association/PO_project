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
        public int mana { get; set; }
        public ManaPotion(Texture2D texture, string name, string description, string rarity, int mana,int quantity,Player player): base(texture, name, description, rarity,quantity,player){
            this.mana = mana;
        }
        public override void Use(){
            if (_playerBinding.mana + mana > _playerBinding.maxMana)
                _playerBinding.mana = _playerBinding.maxMana;
            else
                _playerBinding.mana += mana;
            Quantity--;
        }
    }
}