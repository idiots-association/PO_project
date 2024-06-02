using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Utils;
using System;

namespace PO_game.Src.States
{
    public class SettingsState : State
    {
        private Texture2D _buttonTexture;
        private Button _exitButton;
        public SettingsState(ContentManager content) : base(content) { }
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