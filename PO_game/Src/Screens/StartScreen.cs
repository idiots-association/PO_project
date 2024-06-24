using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Utils;
using System;
using System.IO;

namespace PO_game.Src.Screens
{
    /// <summary>
    /// <c>StatScreen</c> is a class handling the contents of the start screen.
    /// <para> From this screen, the player can start the game, load a game, go to settings or exit the game.</para>
    /// </summary>
    
    public class StartScreen : Screen
    {
        private Texture2D _backgroundTexture;
        private Texture2D _buttonTexture;
        private int buttonSpacing = 55;
        private Button _startButton;
        private Button _exitButton;
        private Button _settingsButton;
        private Button _loadButton;


        public StartScreen(ContentManager content) : base(content) { }
        public override void LoadContent()
        {
            _backgroundTexture = content.Load<Texture2D>("Others/cool_background");
            _buttonTexture = content.Load<Texture2D>("Others/buttonTest");


            _startButton = new Button(_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 - _buttonTexture.Height - buttonSpacing),
                Scale = 3.5f,
                leftClick = new EventHandler(ButtonStart_Click),
                Layer = 0.3f
            };
            _loadButton = new Button(_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2),
                Scale = 3.5f,
                leftClick = new EventHandler(ButtonLoad_Click),
                Layer = 0.3f
            };
            _settingsButton = new Button(_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 + _buttonTexture.Height + buttonSpacing),
                Scale = 3.5f,
                leftClick = new EventHandler(ButtonSettings_Click),
                Layer = 0.3f
            };
            _exitButton = new Button(_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 + (_buttonTexture.Height + buttonSpacing) * 2),
                Scale = 3.5f,
                leftClick = new EventHandler(ButtonExit_Click),
                Layer = 0.3f
            };
        }
        public void ButtonStart_Click(object sender, EventArgs e)
        {
            bool check = true;
            for (int i = 1; i <= 5; i++)
            {
                if (!File.Exists($"save{i}.json"))
                {
                    check = false;
                }
            }
            if (check)
            {
                ScreenManager.Instance.AddScreen(new LoadGameScreen(content));
            }
            else
            {
                ScreenManager.Instance.AddScreen(new GameScreen(content, 0));
            }
        }
        public void ButtonSettings_Click(object sender, EventArgs e)
        {
            ScreenManager.Instance.AddScreen(new SettingsScreen(content));
        }
        public void ButtonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        public void ButtonLoad_Click(object sender, EventArgs e)
        {
            ScreenManager.Instance.AddScreen(new LoadGameScreen(content));
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
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight), Color.White);
            _startButton.Draw(spriteBatch);
            _settingsButton.Draw(spriteBatch);
            _exitButton.Draw(spriteBatch);
            _loadButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}