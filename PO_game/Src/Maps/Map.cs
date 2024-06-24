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

    /// <summary>
    /// <c>Map</c> class to manage the map of the game.
    /// <para>
    /// <list type="bullet">
    /// <item>
    /// <description><c>_background</c> is a dictionary containing tile position of a tile, represented as a number corresponding with specific graphic from the <c>_tileset</c>.</description>
    /// </item>
    /// <item>
    /// <description><c>_collisions</c> is a dictionary containing tile position of a collision, represented as a number corresponding with specific type of collision and a graphic from the <c>_collisionTileset</c></description>
    /// </item>
    /// <item>
    /// <description><c>_enemies_Locations</c> is a dictionary containing tile position of an enemy, represented as a number corresponding with specific type of enemy.</description>
    /// </item>
    /// <item>
    /// <description><c>_enemies</c> is a list of <c>Enemy</c> objects <see cref="Enemy"/>/></description>
    /// </item>
    /// </list>
    /// </para>
    /// </summary>
    public class Map
    {
        private Dictionary<Vector2, int> _background;
        private Dictionary<Vector2, int> _backgroundObjects;
        private Dictionary<Vector2, int> _shadow;
        private Dictionary<Vector2, int> _collisions;
        private Dictionary<Vector2, int> _enemiesLocations;
        private Texture2D _tileset;
        private Texture2D _collisionTileset;
        private List<Enemy> _enemies;
        private MapId _mapId;

        /// <summary>
        /// A method to show collisions.
        /// </summary>
        /// <param name="inputController"></param>
        private void ShowCollisions(InputController inputController)
        {
            if (inputController.isKeyPressed(Keys.C))
            {
                Globals.ShowCollisions = !Globals.ShowCollisions;
            }
        }

        /// <summary>
        /// A method to get the destination of a warp using the player's position.
        /// </summary>
        /// <param name="player"></param>
        /// <returns>Destination of a warp</returns>
        private Tuple<MapId, Vector2> GetWarpDestination(Player player)
        {
            if (Warps.WarpPoints[_mapId].ContainsKey(player.TilePosition))
            {
                return Warps.WarpPoints[_mapId][player.TilePosition];
            }
            return null;
        }


        /// <summary>
        /// Method called in the Update method to check if the player has collided with a warp.
        /// </summary>
        /// <param name="player"></param>
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


        /// <summary>
        /// <c>Map</c> constructor. It loads the map from the csvs files and the tileset from the content.
        /// <para>
        /// Enemy csv file is optional. 
        /// If it exists, it creates enemies from the csv file and calls the <c>UpdateEnemyCollisions</c> method to add enemies collisions to the _collisions map.
        /// </para>
        /// </summary>
        /// <param name="csv_map"></param>
        /// <param name="tileset"></param>
        /// <param name="content"></param>
        public Map(string csv_map, string tileset, ContentManager content, MapId mapId)
        {
            _mapId = mapId;
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

            string shadowCsvPath = csv_map + "_Shadow.csv";
            if (File.Exists(shadowCsvPath))
            {
                _shadow = LoadLayer(shadowCsvPath);
            }
            else
            {
                _shadow = new Dictionary<Vector2, int>();
            }

            string backgroundObjectsCsvPath = csv_map + "_BackgroundObjects.csv";
            if(File.Exists(backgroundObjectsCsvPath))
            {
                _backgroundObjects = LoadLayer(backgroundObjectsCsvPath);
            }
            else
            {
                _backgroundObjects = new Dictionary<Vector2, int>();
            }

        }

        public Dictionary<Vector2, int> GetCollisionsMap()
        {
            return _collisions;
        }

        /// <summary>
        /// Map Update method. Handles the collisions, warps and enemies logic and is called by the Update method in the <c>GameScreen</c> class.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="inputController"></param>
        /// <param name="player"></param>
        public void Update(GameTime gameTime, InputController inputController, Player player)
        {
            ShowCollisions(inputController);
            CheckWarpCollision(player);
            foreach (var enemy in _enemies)
            {
                
            }
        }


        /// <summary>
        /// Method to load a layer from a csv file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>The dictionary witch contains a tile position of an object paired with some number associated with the object type.</returns>
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
        

        /// <summary>
        /// A method to create enemies from the csv file.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>List of <c>Enemy<c> objects associated with this map.</c></c></returns>
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
        public List<Enemy> GetEnemies()
        {
            return _enemies;
        }

        /// <summary>
        /// A method to update the collisions map with the enemies collisions. Called after creating enemies.
        /// </summary>
        private void UpdateEnemyCollisions()
        {
            foreach(var enemy in _enemiesLocations)
            {
                if (enemy.Value > -1)
                {
                    _collisions.Add(enemy.Key, (int)Collision.NoPassCollision);
                }
            }
        }

        /// <summary>
        /// Method to remove enemy from the map after it's defeated.
        /// </summary>
        /// <param name="enemy"></param>
        public void RemoveEnemy(Enemy enemy)
        {
            _collisions.Remove(enemy.TilePosition);
            _enemies.Remove(enemy);

        }

        /// <summary>
        /// A method to draw a layer of the map. It uses the <c>_tileset</c> to draw the tiles.
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="tileset"></param>
        /// <param name="spriteBatch"></param>
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

        /// <summary>
        /// A method to draw the map. It draws the background, collisions (if enabled) and the enteties of the map (including player, enemies and NPCs).
        /// </summary>
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

            DrawLayer(_backgroundObjects, _tileset, spriteBatch);
            DrawLayer(_shadow, _tileset, spriteBatch);

        }

    };
}
