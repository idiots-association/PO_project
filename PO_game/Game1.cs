using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src;
using PO_game.Src.States;
using PO_game.Src.Utils;
using System.Collections.Generic;

namespace PO_game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public StateManager StateManager { get; private set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = GlobalSettings.ScreenWidth;
            _graphics.PreferredBackBufferHeight = GlobalSettings.ScreenHeight;
            _graphics.ApplyChanges();

            StateManager = StateManager.Instance;
        }

        protected override void Initialize()
        {
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            StateManager.Instance.AddState(new StartState(Content));
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();  //do wyrzucenia potem, za to jest odpowiedzialny exit button i wszelkie klasy state

            StateManager.Instance.GetCurrentState().Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            StateManager.Instance.GetCurrentState().Draw(_spriteBatch);
            /*switch(_stateManager.getCurrentState())
            {
                case StartState startState:
                    startState.Draw(_spriteBatch);
                    break;
                case GameState gameState:
                    gameState.Draw(_spriteBatch);
                    break;
            }*/

            base.Draw(gameTime);
        }

    }
}
