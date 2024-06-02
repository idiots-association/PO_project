using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Entities;
using PO_game.Src.Maps;
using PO_game.Src.Utils;
using System;
using System.IO;
using System.Text.Json;

namespace PO_game.Src.States
{
    public class GameState : State
    {
        private InputController _inputController;
        private Camera _camera;
        public MapManager MapManager { get; private set; }
        private Player _player;
        private Matrix _transformMatrix;
        private Matrix _scaleMatrix;
        private Matrix _originTranslationMatrix;
        private Matrix _inverseOriginTranslationMatrix;
        private Button _changeStateButton;
        private Texture2D _buttonTexture;
        private string _savePath;
        private bool _loadingFromSave;

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



        public GameState(ContentManager content, int save) : base(content)
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

        private Vector2 TileToPixelPosition(Vector2 tilePosition)
        {
            return new Vector2(
                (int)(tilePosition.X * Globals.TileSize) + Globals.TileSize / 2,
                tilePosition.Y * Globals.TileSize - 22 // tmp
            );
        }


        private void LoadMaps()
        {
            var tileset = "POtileset";

            var lobby_csv = "../../../Content/Maps/Lobby/MapWithPath";
            var lobby_map = new Map(lobby_csv, tileset, content);
            MapManager.Instance.AddMap(MapId.Lobby, lobby_map);

            var playerPath_csv = "../../../Content/Maps/PlayerPath/PlayerPath";
            var playerPath_map = new Map(playerPath_csv, tileset, content);
            MapManager.Instance.AddMap(MapId.PlayerPath, playerPath_map);
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
            //    Console.Write(_player.TilePosition); 
            //    Console.WriteLine(_player.Sprite.Position);

            MapManager.Instance.GetMap(MapManager.CurrentMap).Update(gameTime, _inputController, _player);
            _camera.Follow(_player);

            Matrix translationMatrix = _camera.Transform;
            _transformMatrix = translationMatrix * _originTranslationMatrix * _scaleMatrix * _inverseOriginTranslationMatrix;
            _changeStateButton.Update();

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: _transformMatrix, samplerState: SamplerState.PointClamp);
            MapManager.Instance.GetMap(MapManager.CurrentMap).Draw(spriteBatch);
            _player.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _changeStateButton.Draw(spriteBatch);
            spriteBatch.End();
        }


    }
}