using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PO_game.Src.States
{
    public abstract class State
    {
        protected ContentManager Content;
        public State( ContentManager content)
        {
            Content = content;
        }
        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void PostUpdate(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}