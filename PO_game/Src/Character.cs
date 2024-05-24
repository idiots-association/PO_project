using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Utils;

namespace PO_game.Src
{
    public class Character
    {
        public Sprite Sprite { get; set; }
        public CharacterState State { get; set; }



        public Character(Sprite sprite)
        {
            Sprite = sprite;
            State = CharacterState.Idle;
        }

        public virtual void Update(GameTime gameTime){}
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.Texture, Sprite.Position, Color.White);
        }

    }
}
