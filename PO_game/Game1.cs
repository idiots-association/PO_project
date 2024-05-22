using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src;
using PO_game.Src.Utils;
using System.Collections.Generic;

namespace PO_game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputController _inputController;

        private Camera _camera;
        private Player _player;
        private List<NPC> _npcs = new List<NPC>();

        private Sprite CreateSprite(Color color, Vector2 position)
        {
            var texture = new Texture2D(GraphicsDevice, GlobalSettings.TileSize, GlobalSettings.TileSize);
            Color[] data = new Color[GlobalSettings.TileSize * GlobalSettings.TileSize];
            for (int i = 0; i < data.Length; ++i) data[i] = color;
            texture.SetData(data);

            return new Sprite(texture, data, position);
        }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _inputController = new InputController();
            _camera = new Camera();

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
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var playerPosition = new Vector2(GlobalSettings.ScreenWidth / 2 - GlobalSettings.TileSize / 2, GlobalSettings.ScreenHeight / 2 - GlobalSettings.TileSize / 2);
            var playerSprite = CreateSprite(Color.Chocolate, playerPosition);
            _player = new Player(playerSprite);

            var npcPosition1 = new Vector2(128, 128); // These are multiples of 32
            var npcSprite1 = CreateSprite(Color.Red, npcPosition1);
            var npc1 = new NPC(npcSprite1);
            _npcs.Add(npc1);

            var npcPosition2 = new Vector2(160, 160); // These are multiples of 32
            var npcSprite2 = CreateSprite(Color.Green, npcPosition2);
            var npc2 = new NPC(npcSprite2);
            _npcs.Add(npc2);
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            _inputController.Update();
            _player.Update(gameTime, _inputController);

            _camera.Follow(_player);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            _player.Draw(_spriteBatch);
            foreach (var npc in _npcs)
            {
                npc.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
