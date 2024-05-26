using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using PO_game.Src.Utils;
using PO_game.Src.Controls;
using System.Collections.Generic;

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
        private Map _lobby;
        private Dictionary<Vector2, int> collisionMap;

        public GameState(ContentManager content): base(content){
            _inputController = new InputController();
            _camera = new Camera();
            _scaleMatrix = Matrix.CreateScale(GlobalSettings.Scale);
            _originTranslationMatrix = Matrix.CreateTranslation(-GlobalSettings.ScreenWidth / 2, -GlobalSettings.ScreenHeight / 2, 0);
            _inverseOriginTranslationMatrix = Matrix.CreateTranslation(GlobalSettings.ScreenWidth / 2, GlobalSettings.ScreenHeight / 2, 0);
        }
        public override void LoadContent()
        {
            var mapLayer1 = "../../../Content/placeholder_map_with_collisions_layer1.csv";
            var collisionLayer = "../../../Content/placeholder_map_with_collisions_collisions.csv";
            var tileset = "grass";
            _lobby = new Map(mapLayer1, collisionLayer, tileset, content);
            var playerPosition = new Vector2(GlobalSettings.ScreenWidth / 2 - GlobalSettings.TileSize / 2, GlobalSettings.ScreenHeight / 2 - GlobalSettings.TileSize / 2);
            var playerTexture = content.Load<Texture2D>("playerxd");
            _player = new Player(new Sprite(playerTexture, playerPosition));

            var npcPosition1 = new Vector2(GlobalSettings.TileSize*8, GlobalSettings.TileSize * 8);
            var npcTexture1 = content.Load<Texture2D>("npc_placeholder");
            var npc1 = new NPC(new Sprite(npcTexture1, npcPosition1));
            _npcs.Add(npc1);

            collisionMap = _lobby.GetCollisionsMap();

        }
        public override void Update(GameTime gameTime)
        {
            _inputController.Update();
            _player.Update(gameTime, _inputController, collisionMap);
            _lobby.Update(gameTime, _inputController);
            _camera.Follow(_player);

            Matrix translationMatrix = _camera.Transform;
            _transformMatrix =  translationMatrix * _originTranslationMatrix * _scaleMatrix * _inverseOriginTranslationMatrix;

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
            spriteBatch.End();
        }
    }
}