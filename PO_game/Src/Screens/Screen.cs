using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PO_game.Src.Screens
{
    /// <summary>
    /// <c>Screen</c> is an abstract class creating interface for all the other Screen classes.
    /// </summary>
    public abstract class Screen
    {
        protected GraphicsDeviceManager graphics;
        protected ContentManager content;
        public Screen(ContentManager content)
        {
            this.content = content;
        }
        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}