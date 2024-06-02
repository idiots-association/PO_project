using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
namespace PO_game.Src.Items
{
    public abstract class Item
    {

        protected Character _characterBinding;
        public Texture2D Texture;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }


        public Item(Texture2D texture, string name, string description, string rarity, Character characterBinding)
        {
            Texture = texture;
            Name = name;
            Description = description;
            Rarity = rarity;
            _characterBinding = characterBinding;
        }
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 position);
    }
}