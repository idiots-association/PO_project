using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Utils;
using System;

namespace PO_game.Src.Screens
{
    /// <summary>
    /// <c>SettingsScreen</c> is a class handling the contents of the settings screen.
    /// <para> For now, it contains only an exit button.</para>
    /// </summary>
    
    public class SettingsScreen : Screen
    {
        private Texture2D _buttonTexture;
        private Button _exitButton;
        public SettingsScreen(ContentManager content) : base(content) { }
        public override void LoadContent()
        {
            _buttonTexture = content.Load<Texture2D>("Others/startButton");

            _exitButton = new Button(_buttonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2),
                Text = "Exit Settings",
                leftClick = new EventHandler(ButtonExit_Click),
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
            spriteBatch.Begin();
            _exitButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}