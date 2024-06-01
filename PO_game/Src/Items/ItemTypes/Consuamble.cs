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
    public abstract class Consumable: Item{

        public int Quantity { get; set; }
        public Consumable(Texture2D texture, string name, string description, string rarity,int quantity, Character character): base(texture, name, description, rarity, character){
            Quantity = quantity;
        }
        public abstract void Use();
        public override void Draw(SpriteBatch spriteBatch, Vector2 position){
            spriteBatch.Draw(Texture, position, Color.White);
    }

}
}