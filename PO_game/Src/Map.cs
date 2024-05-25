using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PO_game.Src.Utils;

namespace PO_game.Src
{
    public class Map
    {
        private Dictionary<Vector2, int> _layer1;
        private Dictionary<Vector2, int> _layer2;
        private Dictionary<Vector2, int> _collisions;
        private Texture2D _tileset;
        private Texture2D _collisionTileset;

        public Map(string layer1, string collisions, string tileset, ContentManager content)
        {
            _layer1 = LoadMap(layer1);
            _tileset = content.Load<Texture2D>(tileset);

            //_layer2 = LoadMap(filename + "_layer2.csv");
            _collisions = LoadMap(collisions);
            _collisionTileset = content.Load<Texture2D>("collisions");
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
                        if(value > -1)
                            result[new Vector2(x, y)] = value;
                    }
                }
                y++;
            }
            return result;
        }

        private void DrawLayer(Dictionary<Vector2, int> layer, Texture2D tileset,  SpriteBatch spriteBatch)
        {
            foreach (var tile in layer)
            {
                Rectangle drect = new(
                    (int)tile.Key.X * GlobalSettings.TileSize,
                    (int)tile.Key.Y * GlobalSettings.TileSize,
                    GlobalSettings.TileSize,
                    GlobalSettings.TileSize
                    );

                int x = tile.Value % (tileset.Width / GlobalSettings.TileSize);
                int y = tile.Value / (tileset.Width / GlobalSettings.TileSize);

                Rectangle srect = new(
                    x * GlobalSettings.TileSize,
                    y * GlobalSettings.TileSize,
                    GlobalSettings.TileSize,
                    GlobalSettings.TileSize
                    );

                spriteBatch.Draw(tileset, drect, srect, Color.White);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawLayer(_layer1, _tileset, spriteBatch);
            DrawLayer(_collisions, _collisionTileset, spriteBatch);
        }

    };
}
