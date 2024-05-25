using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using PO_game.Src.Controls;
using PO_game.Src.Utils;

namespace PO_game.Src.States
{
    public class SettingsState: State
    {
        private Texture2D _buttonTexture;
        private SpriteFont _buttonFont;
        private Button _exitButton;
        private int buttonSpacing = 20;
        public SettingsState(ContentManager content): base(content){}
        public override void LoadContent()
        {
            _buttonTexture = content.Load<Texture2D>("startButton");
            _buttonFont = content.Load<SpriteFont>("Arial"); 
            
            _exitButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth / 2 , GlobalSettings.ScreenHeight / 2),
                Text = "Exit Settings",
                Click = new EventHandler(ButtonExit_Click),
                Layer = 0.3f
            };
            
        }
        
        public void ButtonExit_Click(object sender, EventArgs e)
        {
            StateManager.Instance.RemoveState();
        }
        public override void Update(GameTime gameTime)
        {
            _exitButton.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _exitButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}