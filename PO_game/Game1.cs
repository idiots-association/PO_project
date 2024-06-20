using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.Screens;
using PO_game.Src.Utils;


namespace PO_game
{
    /// <summary>
    /// <c>Game</c> class that initializes and runs the project.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        /// <summary>
        /// <c>ScreenManager</c> instance to manage screens of the game.
        /// </summary>
        public ScreenManager ScreenManager { get; private set; }

        /// <summary>
        /// Constructor of the Game1 class. Sets window settings and content directory.
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            _graphics.ApplyChanges();

        }

        /// <summary>
        /// Monogame Framework method to initialize the game properties. This method is called only once per instance.
        /// </summary>
        protected override void Initialize()
        {
            ScreenManager = ScreenManager.Instance;
            base.Initialize();
        }

        /// <summary>
        /// Monogame Framework method to load content of the game. This method is called only once per instance.
        /// </summary>
        protected override void LoadContent()
        {
            Globals.gameFont = Content.Load<SpriteFont>("Fonts/Arial");
            ScreenManager.Instance.AddScreen(new StartScreen(Content));
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// Monogame Framework method to update logic of the game every frame.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ScreenManager.Instance.GetCurrentScreen().Update(gameTime);

            base.Update(gameTime);
        }


        /// <summary>
        /// Monogame Framework method to draw the game every frame.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            ScreenManager.Instance.GetCurrentScreen().Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
