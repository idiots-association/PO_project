using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
namespace PO_game.Src.Items.Consumables
{
    public class ManaPotion : Consumable
    {
        public int mana { get; set; }
        public ManaPotion(Texture2D texture, string name, string description, string rarity, int mana, int quantity, Character character) : base(texture, name, description, rarity, quantity, character)
        {
            this.mana = mana;
        }
        public override void Use()
        {
            if (_characterBinding.mana + mana > _characterBinding.maxMana)
                _characterBinding.mana = _characterBinding.maxMana;
            else
                _characterBinding.mana += mana;
            Quantity--;
        }
    }
}