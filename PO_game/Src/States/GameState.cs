using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using PO_game.Src.Utils;
using PO_game.Src.Controls;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using PO_game.Src.Maps;

namespace PO_game.Src.States
{
    public class GameState: State
    {
        private InputController _inputController;
        private Camera _camera;
        public MapManager MapManager { get; private set; }
        private Player _player;
        private Matrix _transformMatrix;
        private Matrix _scaleMatrix;
        private Matrix _originTranslationMatrix;
        private Matrix _inverseOriginTranslationMatrix;
        private Map _lobby;
        private Dictionary<Vector2, int> collisionMap;
        private Button _changeStateButton;
        private Texture2D _buttonTexture;
        private string _savePath;
        private bool _loadingFromSave;
        private Vector2 _playerTile;

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



        public GameState(ContentManager content, int save): base(content){
            _inputController = new InputController();
            _camera = new Camera();
            _scaleMatrix = Matrix.CreateScale(Globals.Scale);
            _originTranslationMatrix = Matrix.CreateTranslation(-Globals.ScreenWidth / 2, -Globals.ScreenHeight / 2, 0);
            _inverseOriginTranslationMatrix = Matrix.CreateTranslation(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2, 0);
            _savePath = GenerateSavePath(save);
            _loadingFromSave = File.Exists(_savePath);
            MapManager = MapManager.Instance;

        }

        private Vector2 TileToPixelPosition(Vector2 tilePosition)
        {
            return new Vector2(
                (int)(tilePosition.X * Globals.TileSize) + Globals.TileSize / 2,
                tilePosition.Y * Globals.TileSize - 22 // tmp
            );
        }


        private void LoadMaps()
        {
            var lobby_map = "../../../Content/Maps/Lobby/MapWithPath";
            var tileset = "POtileset";
            _lobby = new Map(lobby_map, tileset, content);

            MapManager.Instance.AddMap(MapId.Lobby, _lobby);
        }
        private void UnloadGame()
        {
             MapManager.Instance.ClearMaps();
            _player = null;
            _camera = null;
            _inputController = null;
        }

        private void SaveGame(object sender, EventArgs e)
        {
            var playerStats = new StatsToSave();
            playerStats.Position = new Vector2Data(_player.TilePosition);
            playerStats.Name = "Player";
            playerStats.CurrentMapId = MapManager.Instance.CurrentMap;

            string serializedStats = JsonSerializer.Serialize<StatsToSave>(playerStats);
            File.WriteAllText(_savePath, serializedStats);

            UnloadGame();
            StateManager.Instance.RemoveState();
        }

        private void LoadFromSave()
        {
            var serializedStats = File.ReadAllText(_savePath);
            var loadedStats = JsonSerializer.Deserialize<StatsToSave>(serializedStats);

            var playerTexture = content.Load<Texture2D>("Sprites/playerxd");
            _player = new Player(new Sprite(playerTexture), loadedStats.Position.ToVector2());

            MapManager.SetCurrentMap(loadedStats.CurrentMapId);
        }

        private void StartNewGame()
        {
            var playerPosition = new Vector2(10, 10);
            var playerTexture = content.Load<Texture2D>("Sprites/playerxd");
            _player = new Player(new Sprite(playerTexture), playerPosition);
            MapManager.SetCurrentMap(MapId.Lobby);
        }


        public override void LoadContent()
        {
            LoadMaps();
            _buttonTexture = content.Load<Texture2D>("Others/startButton");


            if (_loadingFromSave)
            {
                LoadFromSave();
            }
            else
            {
                StartNewGame();
            }

            _changeStateButton = new Button(_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth - _buttonTexture.Width / 2, _buttonTexture.Height / 2),
                Text = "Save and Exit",
                leftClick = new EventHandler(SaveGame),
                Layer = 0.3f
            };
        }
    
        public override void Update(GameTime gameTime)
        {
            _inputController.Update();
            var collisionMap = MapManager.Instance.GetMap(MapManager.CurrentMap).GetCollisionsMap();
            _player.Update(gameTime, _inputController, collisionMap);
            _playerTile = new Vector2(    // tmp
                (int)(_player.Sprite.Position.X / Globals.TileSize), 
                (int)((_player.Sprite.Position.Y + _player.Sprite.Position.Y % Globals.TileSize) / Globals.TileSize)
            );


            MapManager.Instance.GetMap(MapManager.CurrentMap).Update(gameTime, _inputController);
            _camera.Follow(_player);

            Matrix translationMatrix = _camera.Transform;
            _transformMatrix =  translationMatrix * _originTranslationMatrix * _scaleMatrix * _inverseOriginTranslationMatrix;
            _changeStateButton.Update();

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix:_transformMatrix);
            MapManager.Instance.GetMap(MapManager.CurrentMap).Draw(spriteBatch);
            _player.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
            _changeStateButton.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}