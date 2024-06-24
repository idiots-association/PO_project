using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Entities;
using PO_game.Src.Maps;
using PO_game.Src.Utils;

namespace PO_game.Src.Screens;

public class DeathScreen : Screen
{
   
        private Texture2D _buttonTexture;
        private Texture2D _backgroundTexture;
        private Button _exitButton;
        public DeathScreen(ContentManager content) : base(content) { }
        public override void LoadContent()
        {
            _buttonTexture = content.Load<Texture2D>("Others/exitButton");
            _backgroundTexture = content.Load<Texture2D>("Others/deathScreen");
            _exitButton = new Button(_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2 + 7, Globals.ScreenHeight / 2 + 160),
                leftClick = new EventHandler(ButtonExit_Click),
                Scale = 5f,
                Layer = 0.3f
            };

        }

        public void ButtonExit_Click(object sender, EventArgs e)
        {
            ScreenManager.Instance.RemoveScreen();
    }
        public override void Update(GameTime gameTime)
        {
            _exitButton.Update();
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), null , Color.White, 0f, Vector2.Zero, 10f, SpriteEffects.None, 0.1f);
            _exitButton.Draw(spriteBatch);
            spriteBatch.End();
            
        }
}