using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using PO_game.Src;
using PO_game.Src.Items.Consumables;
using PO_game.Src.Utils;
using PO_game.Src.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using PO_game.Src.Items;
using PO_game.Src.States;
namespace PO_game.Src.Inv{
    public class InventorySlot{
        
        public int Id { get; set; }
        public Item Item { get; set; }
        private Texture2D _texture;
        private MouseState currentMouse;
        private MouseState prevMouse;
        public InventorySlot(int id,Texture2D texture,Item item){
            Id = id;
            Item = item;
            _texture = texture;
        }

        public Vector2 Origin 
        { get
            {
                return new Vector2(_texture.Width/2, _texture.Height/2);
            }
        }
        public Vector2 Position { 
            get
            {
                return new Vector2(GlobalSettings.ScreenWidth / 2 - 3 * GlobalSettings.InvSlotSize + (Id % 6 * GlobalSettings.InvSlotSize) - GlobalSettings.TileSize,
                 GlobalSettings.ScreenHeight / 2 - 3 * GlobalSettings.InvSlotSize + GlobalSettings.InvSlotSize * (Id/6) + GlobalSettings.TileSize/2);
            }
         }
        public Rectangle Rectangle{
            get
            {
                return new Rectangle((int)Position.X - (int)Origin.X , (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);
            }
        }
        public void Update()
        {
            prevMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            if(mouseRectangle.Intersects(Rectangle))
            {
                if(currentMouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed)
                {
                    Item = null;
                }
                if(currentMouse.RightButton == ButtonState.Released && prevMouse.RightButton == ButtonState.Pressed)
                {
                    if (Item is Consumable){
                        Consumable temp_item = (Consumable)Item;
                        temp_item.Use();
                        if (temp_item.Quantity == 0){
                            Item = null;
                        }
                        else
                            Item = temp_item;
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch){
            Color semiTransparentWhite = new Color(255, 255, 255, 128); // Half transparent
            spriteBatch.Draw(_texture, Position, semiTransparentWhite);
            if (Item != null){
                Item.Draw(spriteBatch, Position);
            }
        }
    }
}