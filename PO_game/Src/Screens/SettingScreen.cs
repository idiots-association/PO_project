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
        private Texture2D _returnButtonTexture;
        private Texture2D _backgroundTexture;
        private Button _returnButton;
        /// <summary>
        /// Constructor for the SettingsScreen class.
        /// </summary>
        /// <param name="content"></param>
        public SettingsScreen(ContentManager content) : base(content) { }
        public override void LoadContent()
        {
            _backgroundTexture = content.Load<Texture2D>("Others/backgroundTexture");
            _returnButtonTexture = content.Load<Texture2D>("Others/returnButton");

            _returnButton = new Button(_returnButtonTexture)
            {
                Position = new Vector2(Globals.ScreenWidth / 2 , Globals.ScreenHeight / 2),
                Scale = 3f,
                leftClick = new EventHandler(ButtonReturn_Click),
                Layer = 0.3f
            };

        }
        /// <summary>
        /// Returns to the start screen when the exit button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonReturn_Click(object sender, EventArgs e)
        {
            ScreenManager.Instance.RemoveScreen();
        }
        public override void Update(GameTime gameTime)
        {
            _returnButton.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight), Color.White);
            _returnButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}