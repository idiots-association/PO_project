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
        private Texture2D _tileset;
        private Texture2D _collisionTileset;


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
            _background = LoadMap(csv_map + "_Background.csv");
            _tileset = content.Load<Texture2D>("Tilesets/" + tileset);
            _collisions = LoadMap(csv_map + "_Collisions.csv");
            _collisionTileset = content.Load<Texture2D>("Tilesets/collisions");
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

        public Dictionary<Vector2, int> LoadMap(string filename)
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

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawLayer(_background, _tileset, spriteBatch);
            if (Globals.ShowCollisions)
            {
                DrawLayer(_collisions, _collisionTileset, spriteBatch);
            }
        }

    };
}
