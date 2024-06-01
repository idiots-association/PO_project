using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Utils;

namespace PO_game.Src.Entities
{
    public class Character
    {
        public Sprite Sprite { get; set; }
        public CharacterState State { get; set; }
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

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.Texture, Sprite.Position, Color.White);
        }

    }
}
