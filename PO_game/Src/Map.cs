using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PO_game.Src.Utils;
using Microsoft.Xna.Framework.Input;

namespace PO_game.Src
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
                GlobalSettings.ShowCollisions = !GlobalSettings.ShowCollisions;
            }
        }

        public Map(string csv_map, string tileset, ContentManager content)
        {
            _background = LoadMap(csv_map + "_Background.csv");
            _tileset = content.Load<Texture2D>(tileset);
            _collisions = LoadMap(csv_map + "_Collisions.csv");
            _collisionTileset = content.Load<Texture2D>("collisions");
        }

        public Dictionary<Vector2, int> GetCollisionsMap()
        {
           return _collisions;
        }


        public void Update(GameTime gameTime, InputController inputController)
        {
            ShowCollisions(inputController);
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
                    (int)(x * GlobalSettings.TileSize),
                    (int)(y * GlobalSettings.TileSize),
                    GlobalSettings.TileSize - 1, // resolves random lines on the screen, temp solution
                    GlobalSettings.TileSize - 1 // - ,, -
                    );

                spriteBatch.Draw(tileset, drect, srect, Color.White);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawLayer(_background, _tileset, spriteBatch);
            if (GlobalSettings.ShowCollisions)
            {
                DrawLayer(_collisions, _collisionTileset, spriteBatch);
            }
        }

    };
}
