using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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


        public Character(Sprite sprite)
        {
            Sprite = sprite;
            Position = sprite.Position;
        }

        public virtual void Update(GameTime gameTime, InputController inputController)
        {
        }



        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.Texture, Position, Color.White);
        }

    }
}
