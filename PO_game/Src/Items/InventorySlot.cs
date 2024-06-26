using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.Entities;
using PO_game.Src.Items;
using PO_game.Src.Utils;
using System;
using System.Security;
namespace PO_game.Src.Inv
{
    /// <summary>
    /// <c>InventorySlot</c> is a class that represents an inventory slot in the game.
    /// <para>It allows the creation of inventory slots with custom textures and items and allows the player to interact with the items in the inventory.
    /// Any <c>InventorySlot</c> can hold only one type of an item at a time. </para>
    /// </summary>
    public class InventorySlot
    {

        public int id { get; set; }
        public Item item { get; set; }
        private Texture2D _texture;
        private MouseState currentMouse;
        private MouseState prevMouse;
        public Player player;
        public InventorySlot(int id, Texture2D texture, Item item, Player player)
        {
            this.id = id;
            this.item = item;
            _texture = texture;
            this.player = player;
        }

        public Vector2 Origin
        {
            get
            {
                return new Vector2(_texture.Width / 2, _texture.Height / 2);
            }
        }
        public Vector2 Position
        {
            get
            {
                return new Vector2(Globals.ScreenWidth / 2 - 3 * Globals.InvSlotSize + (id % 6 * Globals.InvSlotSize) - Globals.TileSize,
                 Globals.ScreenHeight / 2 - 3 * Globals.InvSlotSize + Globals.InvSlotSize * (id / 6) + Globals.TileSize / 2);
            }
        }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
        
        public void CheckAndRemoveItemIfEmpty()
        {
            if (item is Consumable consumable && consumable.Quantity == 0)
            {
                item = null;
            }
        }
        public void Update()
        {
            prevMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(Rectangle))
            {
                if (currentMouse.RightButton == ButtonState.Released && prevMouse.RightButton == ButtonState.Pressed)
                {
                    item = null;
                }
                if (currentMouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed)
                {
                    switch (item)
                    {
                        case Consumable:
                            Consumable tempConsumable = (Consumable)item;
                            tempConsumable.Use(player);
                            if(tempConsumable.Quantity == 0)
                            {
                                item = null;
                            }
                            else
                            {
                                item = tempConsumable;
                            }
                            break;
                        case Weapon:
                            Console.WriteLine(item.Name);
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
        public void Draw(SpriteBatch spriteBatch)
        {
            Color semiTransparentWhite = new Color(255, 255, 255, 128); // Half transparent
            spriteBatch.Draw(_texture, Position, semiTransparentWhite);
            if (item != null)
            {
                switch (item)
                {
                    case(Weapon):
                        item.Draw(spriteBatch, Position + new Vector2(20,25));
                        break;
                    case(Consumable):
                        item.Draw(spriteBatch, Position );
                        break;
                }
                
            }
        }
    }
}