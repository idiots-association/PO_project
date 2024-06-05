using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.Entities;
using PO_game.Src.Utils;
using System;
using System.Collections.Generic;
using System.IO;


namespace PO_game.Src.Maps
{
    public class Map
    {
        private Dictionary<Vector2, int> _background;
        private Dictionary<Vector2, int> _collisions;
        private Dictionary<Vector2, int> _enemiesLocations;
        private Texture2D _tileset;
        private Texture2D _collisionTileset;
        private List<Enemy> _enemies;


        private void ShowCollisions(InputController inputController)
        {
            if (inputController.isKeyPressed(Keys.C))
            {
                Globals.ShowCollisions = !Globals.ShowCollisions;
            }
        }

        private Tuple<MapId, Vector2> GetWarpDestination(Player player)
        {
            if (Warps.Lobby.ContainsKey(player.TilePosition))
            {
                return Warps.Lobby[player.TilePosition];
            }
            return null;
        }

        private void CheckWarpCollision(Player player)
        {
            if (_collisions.ContainsKey(player.TilePosition) && _collisions[player.TilePosition] == 1)
            {
                var warp = GetWarpDestination(player);
                if (warp != null)
                {
                    MapManager.Instance.SetCurrentMap(warp.Item1);
                    player.UpdatePosition(warp.Item2);
                }
            }
        }

        public Map(string csv_map, string tileset, ContentManager content)
        {
            _background = LoadLayer(csv_map + "_Background.csv");
            _tileset = content.Load<Texture2D>("Tilesets/" + tileset);
            _collisions = LoadLayer(csv_map + "_Collisions.csv");
            _collisionTileset = content.Load<Texture2D>("Tilesets/collisions");

            string enemiesCsvPath = csv_map + "_Enemies.csv";
            if (File.Exists(enemiesCsvPath))
            {
                _enemiesLocations = LoadLayer(enemiesCsvPath);
                _enemies = CreateEnemies(content);
            }
            else
            {
                _enemiesLocations = new Dictionary<Vector2, int>();
                _enemies = new List<Enemy>();
            }

            UpdateEnemyCollisions();
        }

        public Dictionary<Vector2, int> GetCollisionsMap()
        {
            return _collisions;
        }


        public void Update(GameTime gameTime, InputController inputController, Player player)
        {
            ShowCollisions(inputController);
            CheckWarpCollision(player);
        }

        private Dictionary<Vector2, int> LoadLayer(string filename)
        {
            Dictionary<Vector2, int> result = new();

            StreamReader reader = new(filename);

            int y = 0;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > -1)
                            result[new Vector2(x, y)] = value;
                    }
                }
                y++;
            }
            return result;
        }

        private List<Enemy> CreateEnemies(ContentManager content)
        { 
            List<Enemy> enemies = new();

            foreach (var enemy in _enemiesLocations)
            {
                if (enemy.Value == -1)
                    continue;

                EnemyType enemyType = (EnemyType)enemy.Value;
                Vector2 enemyPosition = enemy.Key;
                enemies.Add(EnemyFactory.CreateEnemy(enemyType, enemyPosition, content));
            }


            return enemies;
        }

        private void UpdateEnemyCollisions()
        {
            foreach(var enemy in _enemiesLocations)
            {
                if (enemy.Value > -1)
                {
                    _collisions.Add(enemy.Key, 0);
                }
            }
        }

        private void DrawEnemies(SpriteBatch spriteBatch)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        private void DrawLayer(Dictionary<Vector2, int> layer, Texture2D tileset, SpriteBatch spriteBatch)
        {
            foreach (var tile in layer)
            {
                Rectangle drect = new(
                    (int)tile.Key.X * Globals.TileSize,
                    (int)tile.Key.Y * Globals.TileSize,
                    Globals.TileSize,
                    Globals.TileSize
                    );

                int x = tile.Value % (tileset.Width / (Globals.TileSize));
                int y = tile.Value / (tileset.Width / (Globals.TileSize));

                Rectangle srect = new(
                    x * Globals.TileSize,
                    y * Globals.TileSize,
                    Globals.TileSize,
                    Globals.TileSize
                    );

                spriteBatch.Draw(tileset, drect, srect, Color.White);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            DrawLayer(_background, _tileset, spriteBatch);
            if (Globals.ShowCollisions)
            {
                DrawLayer(_collisions, _collisionTileset, spriteBatch);
            }


            List<Character> gameObjects = new List<Character>(_enemies);

            gameObjects.Add(player);
            gameObjects.Sort((a, b) => a.TilePosition.Y.CompareTo(b.TilePosition.Y));

            foreach (Character gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
        }

    };
}
