using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.Screens;
using PO_game.Src.Utils;


namespace PO_game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public ScreenManager ScreenManager { get; private set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            _graphics.ApplyChanges();

        }

        protected override void Initialize()
        {
            ScreenManager = ScreenManager.Instance;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.gameFont = Content.Load<SpriteFont>("Fonts/Arial");
            ScreenManager.Instance.AddScreen(new StartScreen(Content));
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ScreenManager.Instance.GetCurrentScreen().Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            ScreenManager.Instance.GetCurrentScreen().Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
