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
        private StateManager _stateManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _stateManager = new StateManager();

            _graphics.PreferredBackBufferWidth = GlobalSettings.ScreenWidth;
            _graphics.PreferredBackBufferHeight = GlobalSettings.ScreenHeight;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _stateManager.AddState(new StartState(Content, _stateManager));
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();  //do wyrzucenia potem, za to jest odpowiedzialny exit button i wszelkie klasy state
            
            _stateManager.getCurrentState().Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _stateManager.getCurrentState().Draw(_spriteBatch);
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
