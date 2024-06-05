using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Utils;

namespace PO_game.Src.Entities
{
    public class Character
    {
        public Sprite Sprite { get; set; }
        public CharacterState State { get; set; }
        public int maxHealth { get; set; }
        public int health { get; set; }
        public int maxMana { get; set; }
        public int mana { get; set; }
        public Vector2 TilePosition { get; set; }



        public Character(Sprite sprite, Vector2 tilePosition)
        {
            Sprite = sprite;
            TilePosition = tilePosition;
            Sprite.Position = new Vector2(
                TilePosition.X * Globals.TileSize + Globals.TileSize / 2 + Sprite.Texture.Width % Globals.TileSize,
                TilePosition.Y * Globals.TileSize - Sprite.Texture.Height % Globals.TileSize);
            State = CharacterState.Idle;
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(Sprite.Position.X - Sprite.Texture.Width / 2f, Sprite.Position.Y - Sprite.Texture.Height / 2f + Sprite.Texture.Height / 2f);
            spriteBatch.Draw(Sprite.Texture, position, Color.White);
        }

    }
}
