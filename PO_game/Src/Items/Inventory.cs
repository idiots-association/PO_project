using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public List<InventorySlot> slots { get; set; }
        public static int Capacity = 36;
        public int currentCapacity { get; set; }
        private Texture2D _texture;
        public bool showInventory { get; set; }
        public Player player { get; set; }
        public Inventory(Texture2D texture, Player player){
            slots = new List<InventorySlot>();
            for (int i = 0; i < Capacity; i++){
                slots.Add(new InventorySlot(i, texture, null, player));
            }
            _texture = texture;
            showInventory = false;
        }
        public void AddItem(Consumable item){
            if (Capacity <= currentCapacity){
                Debug.WriteLine("Inventory is full!");
                return;
            }
            foreach (InventorySlot slot in slots){
                if (slot.item == item){
                    Consumable temp_item = (Consumable)slot.item;
                    temp_item.Quantity += 1;
                    slot.item = temp_item;
                    return;
                }
                else if (slot.item == null){
                    slot.item = item;
                    return;
                }
            }
        }
        public void AddItem(Weapon item){
            if (Capacity <= currentCapacity){
                Debug.WriteLine("Inventory is full!");
                return;
            }
            foreach (InventorySlot slot in slots){
                 if (slot.item == null){
                    slot.item = item;
                    return;
                }
            }
        }
        public void Update(){
            foreach (InventorySlot slot in slots){
                if (slot.item != null){
                    slot.Update();
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch){
            for (int i = 0; i < slots.Count; i++){
                slots[i].Draw(spriteBatch);
            }
        }

    }
}