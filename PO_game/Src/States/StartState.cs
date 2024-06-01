using System;
using System.Collections.Generic;
using System.IO;
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
        private int buttonSpacing = 20;
        private Button _startButton;
        private Button _exitButton;
        private Button _settingsButton;
        private Button _loadButton;
        

        public StartState(ContentManager content) : base(content){}
        public override void LoadContent()
        {   
            _backgroundTexture = content.Load<Texture2D>("Others/cool_background");
            _buttonTexture = content.Load<Texture2D>("Others/startButton");


            _startButton = new Button(_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2 , Globals.ScreenHeight / 2 - _buttonTexture.Height - buttonSpacing),
                Text = "Start Game",
                leftClick = new EventHandler(ButtonStart_Click),
                Layer = 0.3f
            };
            _loadButton = new Button(_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2 , Globals.ScreenHeight / 2 ),
                Text = "Load Game",
                leftClick = new EventHandler(ButtonLoad_Click),
                Layer = 0.3f
            };
            _settingsButton = new Button (_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2 , Globals.ScreenHeight / 2 + _buttonTexture.Height + buttonSpacing),
                Text = "Settings",
                leftClick = new EventHandler(ButtonSettings_Click),
                Layer = 0.3f
            };
            _exitButton = new Button(_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2 , Globals.ScreenHeight / 2  + (_buttonTexture.Height + buttonSpacing)*2),
                Text = "Exit Game",
                leftClick = new EventHandler(ButtonExit_Click),
                Layer = 0.3f
            }; 
        }
        public void ButtonStart_Click(object sender, EventArgs e)
        {
            bool check = true;
            for(int i = 1; i <= 5; i++)
            {
                if (!File.Exists($"save{i}.json"))
                {
                    check = false;
                }
            }
            if (check)
            { 
                StateManager.Instance.AddState(new LoadGameState(content));
            }
            else
            {
               StateManager.Instance.AddState(new GameState(content, 0));
            }
        }
        public void ButtonSettings_Click(object sender, EventArgs e)
        {
            StateManager.Instance.AddState(new SettingsState(content));
        }
        public void ButtonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        public void ButtonLoad_Click(object sender, EventArgs e)
        {
            StateManager.Instance.AddState(new LoadGameState(content));
        }
        public override void Update(GameTime gameTime)
        {
            _startButton.Update();
            _settingsButton.Update();
            _exitButton.Update();
            _loadButton.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0,0,Globals.ScreenWidth,Globals.ScreenHeight), Color.White);
            _startButton.Draw(spriteBatch);
            _settingsButton.Draw(spriteBatch);
            _exitButton.Draw(spriteBatch);
            _loadButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}