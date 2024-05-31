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
using System.Collections;
namespace PO_game.Src.Inv{
    public class InventorySlot{
        
        public int id { get; set; }
        public Item item { get; set; }
        private Texture2D _texture;
        private MouseState currentMouse;
        private MouseState prevMouse;

        public Player player { get; set; }
        public InventorySlot(int id,Texture2D texture,Item item, Player player){
            this.id = id;
            this.item = item;
            _texture = texture;
            this.player = player;
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
                return new Vector2(GlobalSettings.ScreenWidth / 2 - 3 * GlobalSettings.InvSlotSize + (id % 6 * GlobalSettings.InvSlotSize) - GlobalSettings.TileSize,
                 GlobalSettings.ScreenHeight / 2 - 3 * GlobalSettings.InvSlotSize + GlobalSettings.InvSlotSize * (id/6) + GlobalSettings.TileSize/2);
            }
         }
        public Rectangle Rectangle{
            get
            {
                return new Rectangle((int)Position.X , (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        public void Update()
        {
            prevMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            if(mouseRectangle.Intersects(Rectangle))
            {
                if(currentMouse.RightButton == ButtonState.Released && prevMouse.RightButton == ButtonState.Pressed)
                {
                    Console.WriteLine("Clicked on slot " + id);
                    item = null;
                }
                if (currentMouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed)
                {
                    switch (item)
                    {
                        case Consumable:
                            Consumable tempConsumable = (Consumable)item;
                            Console.WriteLine("Consumable used" + tempConsumable.Quantity);
                            tempConsumable.Use();
                            if (tempConsumable.Quantity == 0)
                            {
                                item = null;
                            }
                            else
                            {
                                item = tempConsumable;
                            }
                            break;
                        case Weapon:
                            Weapon tempWeapon = (Weapon)item;
                            if (player.weapon != null)
                            {
                                item = player.weapon;
                            }
                            else
                            {
                                item = null;
                            }
                            player.weapon = tempWeapon;
                            break;
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch){
            Color semiTransparentWhite = new Color(255, 255, 255, 128); // Half transparent
            spriteBatch.Draw(_texture, Position, semiTransparentWhite);
            if (item != null){
                item.Draw(spriteBatch, Position);
            }
        }
    }
}