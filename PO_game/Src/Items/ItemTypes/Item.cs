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
    public abstract class Item{

        protected Player _playerBinding;
        public Texture2D Texture;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }


        public Item(Texture2D texture, string name, string description, string rarity, Player playerBinding){
            Texture = texture;
            Name = name;
            Description = description;
            Rarity = rarity;
        }
        public abstract void Draw(SpriteBatch spriteBatch,Vector2 position);
    }
}