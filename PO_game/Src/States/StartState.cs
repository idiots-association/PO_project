using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.States;
using PO_game.Src.Controls;
using System.Net;
using PO_game.Src.Utils;

namespace PO_game.Src.States
{
    public class StartState : State
    {
        private Texture2D _backgroundTexture;
        private Texture2D _buttonTexture;
        private SpriteFont _buttonFont;
        private int buttonSpacing = 20;
        private Button _startButton;
        private Button _exitButton;
        private Button _settingsButton;
        

        public StartState(ContentManager content) : base(content){}
        public override void LoadContent()
        {   
            _backgroundTexture = content.Load<Texture2D>("cool_background");
            _buttonTexture = content.Load<Texture2D>("startButton");
            _buttonFont = content.Load<SpriteFont>("Arial");  


            _startButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth / 2 , GlobalSettings.ScreenHeight / 2 - _buttonTexture.Height - buttonSpacing),
                Text = "Start Game",
                Click = new EventHandler(ButtonStart_Click),
                Layer = 0.3f
            };
            _settingsButton = new Button (_buttonTexture, _buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth / 2 , GlobalSettings.ScreenHeight / 2 ),
                Text = "Settings",
                Click = new EventHandler(ButtonSettings_Click),
                Layer = 0.3f
            };
            _exitButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth / 2 , GlobalSettings.ScreenHeight / 2  + _buttonTexture.Height + buttonSpacing),
                Text = "Exit Game",
                Click = new EventHandler(ButtonExit_Click),
                Layer = 0.3f
            }; 
        }
        public void ButtonStart_Click(object sender, EventArgs e)
        {
            StateManager.Instance.AddState(new GameState(content));
        }
        public void ButtonSettings_Click(object sender, EventArgs e)
        {
            StateManager.Instance.AddState(new SettingsState(content));
        }
        public void ButtonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        public override void Update(GameTime gameTime)
        {
            _startButton.Update();
            _settingsButton.Update();
            _exitButton.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0,0,GlobalSettings.ScreenWidth,GlobalSettings.ScreenHeight), Color.White);
            _startButton.Draw(spriteBatch);
            _settingsButton.Draw(spriteBatch);
            _exitButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}