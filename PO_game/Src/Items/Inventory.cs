using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
using PO_game.Src.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace PO_game.Src.Inv
{
    /// <summary>
    /// <c>Inventory</c> is a class that represents an inventory in the game.
    /// <para>
    /// It allows the creation of inventories with custom textures and player references and consists of 36 <c>InventorySlots.</c>
    /// <c>Consumable</c> items are stackable, <c>Weapon</c>s are not.</para>
    /// </summary>
    public class Inventory
    {
        public List<InventorySlot> slots { get; set; }
        public static int Capacity = 36;
        public int currentCapacity { get; set; }
        private Texture2D _texture;
        public bool showInventory { get; set; }

        public Inventory(Texture2D texture, Player player)
        {
            slots = new List<InventorySlot>();
            for (int i = 0; i < Capacity; i++)
            {
                slots.Add(new InventorySlot(i, texture, null, player));
            }
            _texture = texture;
            showInventory = false;
        }
        public void AddItem(Item item)
        {
            if (item is Consumable)
            {
                AddItemC((Consumable)item);
            }
            else if (item is Weapon)
            {
                AddItemW((Weapon)item);
            }   
        }
        public void AddItemC(Consumable item)
        {
            if (Capacity <= currentCapacity)
            {
                Debug.WriteLine("Inventory is full!");
                return;
            }
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == null)
                {
                    slot.item = item;
                    currentCapacity += 1;
                    return;
                }
                else if (slot.item.Name == item.Name && slot.item.Rarity == item.Rarity) 
                { // might add some item id later, just to make it more elegant
                    Consumable temp_item = (Consumable)slot.item;
                    temp_item.Quantity += 1;
                    slot.item = temp_item;
                    return;
                }

            }
        }
        public void AddItemW(Weapon item)
        {
            if (Capacity <= currentCapacity)
            {
                Debug.WriteLine("Inventory is full!");
                return;
            }
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == null)
                {
                    slot.item = item;
                    currentCapacity += 1;
                    return;
                }
            }
        }
        public void Update()
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.item != null)
                {
                    slot.Update();
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (showInventory)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    slots[i].Draw(spriteBatch);
                }
            }
        }

    }
}