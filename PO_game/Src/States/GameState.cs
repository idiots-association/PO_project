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
        private InputController inputController;
        private Camera camera;
        private Player player;
        private List<NPC> npcs = new List<NPC>();

        public GameState(ContentManager content, StateManager stateManager): base(content, stateManager){
            inputController = new InputController();
            camera = new Camera();
        }
        public override void LoadContent()
        {
            var playerPosition = new Vector2(GlobalSettings.ScreenWidth / 2 - GlobalSettings.TileSize / 2, GlobalSettings.ScreenHeight / 2 - GlobalSettings.TileSize / 2);
            var playerTexture = content.Load<Texture2D>("player_placeholder");
            player = new Player(new Sprite(playerTexture, playerPosition));

            var npcPosition1 = new Vector2(128, 128); // These are multiples of 32
            var npcTexture1 = content.Load<Texture2D>("npc_placeholder");
            var npc1 = new NPC(new Sprite(npcTexture1, npcPosition1));
            npcs.Add(npc1);

        }
        public override void Update(GameTime gameTime)
        {
            inputController.Update();
            player.Update(gameTime, inputController);

            camera.Follow(player);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: camera.Transform);
            player.Draw(spriteBatch);
            foreach (var npc in npcs)
            {
                npc.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}