using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using PO_game.Src.Items;
using PO_game.Src.Inv;
using PO_game.Src.Utils;
using PO_game.Src.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using PO_game.Src.Items.Consumables;

namespace PO_game.Src.States
{
    public class GameState: State
    {
        private InputController _inputController;
        private Camera _camera;
        private Player _player;
        private Matrix _transformMatrix;
        private Matrix _scaleMatrix;
        private Matrix _originTranslationMatrix;
        private Matrix _inverseOriginTranslationMatrix;
        private List<NPC> _npcs = new List<NPC>();
        private List<Enemy> _enemies = new List<Enemy>();
        private Map _lobby;
        private Dictionary<Vector2, int> collisionMap;
        private Button _changeStateButton;
        private Texture2D _buttonTexture;
        private SpriteFont _buttonFont;
        private StatsToSave _playerStats;
        private string _savePath = "";
        private bool _loadingFromSave;
        private Vector2 _playerTile;

        public GameState(ContentManager content, int safe): base(content){
            _inputController = new InputController();
            _camera = new Camera();
            _scaleMatrix = Matrix.CreateScale(GlobalSettings.Scale);
            _originTranslationMatrix = Matrix.CreateTranslation(-GlobalSettings.ScreenWidth / 2, -GlobalSettings.ScreenHeight / 2, 0);
            _inverseOriginTranslationMatrix = Matrix.CreateTranslation(GlobalSettings.ScreenWidth / 2, GlobalSettings.ScreenHeight / 2, 0);
            if (safe == 0)
            {
                bool ifExist = true;
                int i = 1;
                while(ifExist)
                {
                    string path = $"save{i}.json";
                    if (File.Exists(path))
                    {
                        i++;
                    }
                    else
                    {
                        ifExist = false;
                        _savePath = path;
                    }
                }
            }
            else
            {
                _savePath = $"save{safe}.json";
            }
            _loadingFromSave = File.Exists(_savePath);
        }
        public override void LoadContent()
        {
            var inventoryTexture = content.Load<Texture2D>("inv_slot_grey");
            var playerTexture = content.Load<Texture2D>("playerxd");
            if (_loadingFromSave)
            {
                _playerStats = LoadGame();
                _player = new Player(new Sprite(playerTexture, _playerStats.position.ToVector2()), new Inventory(inventoryTexture, _player));
            }
            else
            {
                var playerPosition = new Vector2(GlobalSettings.ScreenWidth / 2 - GlobalSettings.TileSize / 2, GlobalSettings.ScreenHeight / 2 - GlobalSettings.TileSize / 2);
                _player = new Player(new Sprite(playerTexture, playerPosition), new Inventory(inventoryTexture, _player));
            }

            var mapLayer1 = "../../../Content/placeholder_map_with_collisions_layer1.csv";
            var collisionLayer = "../../../Content/placeholder_map_with_collisions_collisions.csv";
            var tileset = "grass";
            _lobby = new Map(mapLayer1, collisionLayer, tileset, content);

            var enemyPosition1 = new Vector2(GlobalSettings.ScreenWidth / 2 - GlobalSettings.TileSize / 2 - 40, GlobalSettings.ScreenHeight / 2 - GlobalSettings.TileSize / 2);
            var enemyTexture1 = content.Load<Texture2D>("playerxd");
            var enemy1 = new Enemy(new Sprite(enemyTexture1, enemyPosition1), 50);
            _enemies.Add(enemy1);
            
            var npcPosition1 = new Vector2(GlobalSettings.ScreenWidth / 2 - GlobalSettings.TileSize / 2  + 40, GlobalSettings.ScreenHeight / 2 - GlobalSettings.TileSize / 2);
            var npcTexture1 = content.Load<Texture2D>("npc_placeholder");
            var npc1 = new NPC(new Sprite(npcTexture1, npcPosition1));
            _npcs.Add(npc1);


            collisionMap = _lobby.GetCollisionsMap();

            _buttonTexture = content.Load<Texture2D>("startButton");
            _buttonFont = content.Load<SpriteFont>("Arial"); 
            
            _changeStateButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth - _buttonTexture.Width/2, _buttonTexture.Height/2),
                Text = "Save and Exit",
                leftClick = new EventHandler(ChangeStateButton_Click),
                Layer = 0.3f
            };
        }   

        private bool CheckWarp()
        {
            if (collisionMap.ContainsKey(_playerTile) && collisionMap[_playerTile] == 1)
            {
                return true;
            }
            return false;
        }
        
        
        private void ChangeStateButton_Click(object sender, EventArgs e)
        {
            _playerStats = new StatsToSave();
            {
                _playerStats.position = new Vector2Data(_player.Sprite.Position);
                _playerStats.name = "Player";
            }
            SaveGame(_playerStats);
            StateManager.Instance.RemoveState();

        }
        
        public override void Update(GameTime gameTime)
        {
            _inputController.Update();
            _player.Update(gameTime, _inputController, collisionMap);
            _playerTile = new Vector2(    // tmp
                (int)(_player.Sprite.Position.X / GlobalSettings.TileSize), 
                (int)((_player.Sprite.Position.Y + _player.Sprite.Position.Y % GlobalSettings.TileSize) / GlobalSettings.TileSize)
            );
            if (CheckWarp())
            {
                StateManager.Instance.AddState(new FightingState(content, _player, _enemies[0]));
            }
            _lobby.Update(gameTime, _inputController);
            _camera.Follow(_player);

            Matrix translationMatrix = _camera.Transform;
            _transformMatrix =  translationMatrix * _originTranslationMatrix * _scaleMatrix * _inverseOriginTranslationMatrix;
            _changeStateButton.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix:_transformMatrix);
            _lobby.Draw(spriteBatch);
            _player.Draw(spriteBatch);
            foreach (var npc in _npcs)
            {
                npc.Draw(spriteBatch);
            }
            foreach (var enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }
            spriteBatch.End();
            spriteBatch.Begin();
            _changeStateButton.Draw(spriteBatch);
            _player.inventory.Draw(spriteBatch);
            spriteBatch.End();
        }
        
        private void SaveGame(StatsToSave playerStats)
        {
            string serializedStats = JsonSerializer.Serialize<StatsToSave>(playerStats);
            File.WriteAllText(_savePath, serializedStats);
        }
        
        private StatsToSave LoadGame()
        {
            var serializedStats = File.ReadAllText(_savePath);
            return JsonSerializer.Deserialize<StatsToSave>(serializedStats);
        }
    }
}