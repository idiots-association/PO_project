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
        private Texture2D backgroundTexture;
        private Button startButton;
        private Button exitButton;
        private Button settingsButton;

        public StartState(ContentManager content, StateManager stateManager) : base(content, stateManager){}
        public override void LoadContent()
        {   
            backgroundTexture = content.Load<Texture2D>("cool_background");
            var buttonTexture = content.Load<Texture2D>("startButton");
            var buttonFont = content.Load<SpriteFont>("Arial");  

            int buttonSpacing = 20;

            startButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth / 2 , GlobalSettings.ScreenHeight / 2 - buttonTexture.Height - buttonSpacing),
                Text = "Start Game",
                Click = new EventHandler(ButtonStart_Click),
                Layer = 0.3f
            };
            settingsButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth / 2 , GlobalSettings.ScreenHeight / 2 ),
                Text = "Settings",
                Click = new EventHandler(ButtonSettings_Click),
                Layer = 0.3f
            };
            exitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth / 2 , GlobalSettings.ScreenHeight / 2  + buttonTexture.Height + buttonSpacing),
                Text = "Exit Game",
                Click = new EventHandler(ButtonExit_Click),
                Layer = 0.3f
            }; 
        }
        public void ButtonStart_Click(object sender, EventArgs e)
        {
            stateManager.AddState(new GameState(content, stateManager));
        }
        public void ButtonSettings_Click(object sender, EventArgs e)
        {
            stateManager.AddState(new SettingsState(content, stateManager));
        }
        public void ButtonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        public override void Update(GameTime gameTime)
        {
            startButton.Update();
            settingsButton.Update();
            exitButton.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Rectangle(0,0,GlobalSettings.ScreenWidth,GlobalSettings.ScreenHeight), Color.White);
            startButton.Draw(spriteBatch);
            settingsButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}