using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PO_game.Src.States
{
    public abstract class State
    {
        protected GraphicsDeviceManager graphics;
        protected ContentManager content;
        protected StateManager stateManager;
        public State(ContentManager content, StateManager stateManager)
        {
            this.content = content;
            this.stateManager = stateManager;
        }
        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}