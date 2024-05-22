using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Utils;

namespace PO_game.Src
{
    public class Character
    {
        public Sprite Sprite { get; set; }
        public Vector2 Position
        {
            get { return Sprite.Position; }
            set { Sprite.Position = value; }
        }

        public CharacterState State { get; set; }



        public Character(Sprite sprite)
        {
            Sprite = sprite;
            Position = sprite.Position;
            State = CharacterState.Idle;
        }

        public virtual void Update(GameTime gameTime)
        {
        }



        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.Texture, Position, Color.White);
        }

    }
}
