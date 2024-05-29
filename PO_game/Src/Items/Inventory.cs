using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using PO_game.Src;
using PO_game.Src.Items;
using PO_game.Src.Utils;
using PO_game.Src.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace PO_game.Src.Inv{
    public class Inventory{
        public List<InventorySlot> Slots { get; set; }
        public static int Capacity = 36;
        public int currentCapacity { get; set; }
        private Texture2D _texture;
        public bool ShowInventory { get; set; }
        public Inventory(Texture2D texture){
            Slots = new List<InventorySlot>();
            for (int i = 0; i < Capacity; i++){
                Slots.Add(new InventorySlot(i, texture, null));
            }
            _texture = texture;
            ShowInventory = false;
        }
        public void AddItem(Consumable item){
            if (Capacity <= currentCapacity){
                Debug.WriteLine("Inventory is full!");//placeholder
                return;
            }
            foreach (InventorySlot slot in Slots){
                if (slot.Item == item){
                    Consumable temp_item = (Consumable)slot.Item;
                    temp_item.Quantity += item.Quantity;
                    slot.Item = temp_item;
                    return;
                }
                else if (slot.Item == null){
                    slot.Item = item;
                    return;
                }
            }
        }
        public void AddItem(Weapon item){
            if (Capacity <= currentCapacity){
                Debug.WriteLine("Inventory is full!");//placeholder
                return;
            }
            foreach (InventorySlot slot in Slots){
                 if (slot.Item == null){
                    slot.Item = item;
                    return;
                }
            }
        }
        public void Update(){
            foreach (InventorySlot slot in Slots){
                if (slot.Item != null){
                    slot.Update();
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch){
            for (int i = 0; i < Slots.Count; i++){
                Slots[i].Draw(spriteBatch);
            }
        }

    }
}