using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Entities;
using PO_game.Src.Items;
using PO_game.Src.Inv;
using PO_game.Src.Maps;
using PO_game.Src.Utils;
using System;
using System.IO;
using System.Text.Json;

namespace PO_game.Src.Screens
{

    /// <summary>
    /// <c>GameScreen</c> is a class handling the contents of the game.
    /// </summary>
    public class GameScreen : Screen
    {
        private InputController _inputController;
        private Camera _camera;
        public MapManager MapManager { get; private set; }
        private Player _player;
        private Matrix _transformMatrix;
        private Matrix _scaleMatrix;
        private Matrix _originTranslationMatrix;
        private Matrix _inverseOriginTranslationMatrix;
        private Button _saveExitButton;
        private string _savePath;
        private bool _loadingFromSave;
        private Texture2D _inventoryTexture;
        private Texture2D _buttonTexture;
        private HealthBar _healthBar;

        private string GenerateSavePath(int save)
        {
            if (save == 0)
            {
                bool ifExist = true;
                int i = 1;
                while (ifExist)
                {
                    string path = $"save{i}.json";
                    if (File.Exists(path))
                    {
                        i++;
                    }
                    else
                    {
                        ifExist = false;
                        return path;
                    }
                }
            }
            else
            {
                return $"save{save}.json";
            }
            return null;
        }


        /// <summary>
        /// Constructor for the GameScreen class.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="save"></param>
        public GameScreen(ContentManager content, int save) : base(content)
        {
            _inputController = new InputController();
            _camera = new Camera();
            _scaleMatrix = Matrix.CreateScale(Globals.Scale);
            _originTranslationMatrix = Matrix.CreateTranslation(-Globals.ScreenWidth / 2, -Globals.ScreenHeight / 2, 0);
            _inverseOriginTranslationMatrix = Matrix.CreateTranslation(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2, 0);
            _savePath = GenerateSavePath(save);
            _loadingFromSave = File.Exists(_savePath);
            MapManager = MapManager.Instance;
        }

        private void LoadMaps()
        {
            var tileset = "POtileset";
            var burnedtileset = "Burnedtileset";


#if DEBUG
            var lobby_csv = "../../../Content/Maps/Lobby/Lobby";
            var playerPath_csv = "../../../Content/Maps/PlayerPath/PlayerPath";
            var darkForest_csv = "../../../Content/Maps/DarkForest/DarkForest";
            var dragonPit_csv = "../../../Content/Maps/DragonPit/DragonPit";
#else
            var lobby_csv = "Content/Maps/Lobby/Lobby";
            var playerPath_csv = "Content/Maps/PlayerPath/PlayerPath";
            var darkForest_csv = "Content/Maps/DarkForest/DarkForest";
            var dragonPit_csv = "Content/Maps/DragonPit/DragonPit";
#endif


            var lobby_map = new Map(lobby_csv, tileset, content, MapId.Lobby);
            MapManager.Instance.AddMap(MapId.Lobby, lobby_map);


            var playerPath_map = new Map(playerPath_csv, tileset, content, MapId.PlayerPath);
            MapManager.Instance.AddMap(MapId.PlayerPath, playerPath_map);

            var darkForest_map = new Map(darkForest_csv, tileset, content, MapId.DarkForest);
            MapManager.Instance.AddMap(MapId.DarkForest, darkForest_map);

            var dragonPit_map = new Map(dragonPit_csv, burnedtileset, content, MapId.DragonPit);
            MapManager.Instance.AddMap(MapId.DragonPit, dragonPit_map);

        }
        private void UnloadGame()
        {
            MapManager.Instance.ClearMaps();
            _player = null;
            _camera = null;
            _inputController = null;
        }
        
        /// <summary>
        /// A method that saves the game to a file.
        /// </summary>
        
        private void SaveGame(object sender, EventArgs e)
        {
            var playerStats = new StatsToSave();
            playerStats.Position = new Vector2Data(_player.TilePosition);
            playerStats.Name = "Player";
            playerStats.Health = _player.health;
            playerStats.CurrentMapId = MapManager.Instance.CurrentMap;

            string serializedStats = JsonSerializer.Serialize<StatsToSave>(playerStats);
            File.WriteAllText(_savePath, serializedStats);

            UnloadGame();
            ScreenManager.Instance.RemoveScreen();
        }

        /// <summary>
        /// A method that loads the game from a save file.
        /// </summary>
        private void LoadFromSave()
        {
            var serializedStats = File.ReadAllText(_savePath);
            var loadedStats = JsonSerializer.Deserialize<StatsToSave>(serializedStats);

            var playerTexture = content.Load<Texture2D>("Sprites/hero");
            _player = new Player(new Sprite(playerTexture), loadedStats.Position.ToVector2(),_inventoryTexture, content);
            _player.health = loadedStats.Health;

            MapManager.SetCurrentMap(loadedStats.CurrentMapId);
        }

        private void StartNewGame()
        {
            var playerPosition = new Vector2(15, 15);
            var playerTexture = content.Load<Texture2D>("Sprites/hero");
            _player = new Player(new Sprite(playerTexture), playerPosition, _inventoryTexture, content);
            MapManager.SetCurrentMap(MapId.Lobby);
        }


        public override void LoadContent()
        {
            LoadMaps();
            _buttonTexture = content.Load<Texture2D>("Others/save&exitButton");
            _inventoryTexture = content.Load<Texture2D>("Items/inv_slot_grey");

            if (_loadingFromSave)
            {
                LoadFromSave();
            }
            else
            {
                StartNewGame();
            }

            _saveExitButton = new Button(_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth - _buttonTexture.Width * (float)1.5, _buttonTexture.Height * (float)1.5),
                Scale = 3f,
                leftClick = new EventHandler(SaveGame),
                Layer = 0.3f
            };

            _healthBar = new HealthBar(content, new(10, 10), _player.maxHealth);
        }


        /// <summary>
        /// Update method called by Update in Game1 class.
        /// <para>
        /// It handles input controller logic, all entities updates, camera following the player, map updates and button updates.
        /// </para>
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _inputController.Update();
            var collisionMap = MapManager.Instance.GetMap(MapManager.CurrentMap).GetCollisionsMap();
            _player.Update(gameTime, _inputController, collisionMap);
            foreach (var enemy in MapManager.Instance.GetMap(MapManager.CurrentMap).GetEnemies())
            {
                enemy.Update(content,_player,_inputController);
            }

            MapManager.Instance.GetMap(MapManager.CurrentMap).Update(gameTime, _inputController, _player);
            _camera.Follow(_player);

            Matrix translationMatrix = _camera.Transform;
            _transformMatrix = translationMatrix * _originTranslationMatrix * _scaleMatrix * _inverseOriginTranslationMatrix;
            _healthBar.Update(_player.health);
            _saveExitButton.Update();
            
            if ( _player != null)
            {
                if (_player.health <= 0)
                {
                    ScreenManager.Instance.RemoveScreen();
                    UnloadGame();
                    if (File.Exists(_savePath))
                    {
                        File.Delete(_savePath);
                    }
                    ScreenManager.Instance.AddScreen(new DeathScreen(content));
                }
            }
        }

        /// <summary>
        /// Draw method called by Draw in Game1 class.
        /// <para>
        /// It calls the Draw method of the current map, the player inventory and the change state button.
        /// </para>
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: _transformMatrix, samplerState: SamplerState.PointClamp);
            MapManager.Instance.GetMap(MapManager.CurrentMap).Draw(spriteBatch, _player);
            spriteBatch.End();

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _saveExitButton.Draw(spriteBatch);
            _player.inventory.Draw(spriteBatch);
            _healthBar.Draw(spriteBatch);
            spriteBatch.End();
        }


    }
}