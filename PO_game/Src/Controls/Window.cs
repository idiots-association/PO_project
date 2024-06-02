using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Utils;

namespace PO_game.Src.Controls
{
    public class Window
    {
        #region Fields
        private ContentManager content;
        private bool isHovering;
        private Texture2D texture;
        private Texture2D buttontexture;
        private Texture2D buttontexture2;
        public Button _exitButton;
        public Button _exitButton2;
        private int number_of_buttons;
        #endregion

        #region Properties

        public float Layer { get; set; }

        public Vector2 Origin
        {
            get { return new Vector2(texture.Width / 2, texture.Height / 2); }
        }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, texture.Width,
                    texture.Height);
            }
        }

        public string Text { get; set; }

        #endregion

        #region Methods
        public void buttonUpdate()
        {
            switch (number_of_buttons)
            {
                case 1:
                    buttontexture = content.Load<Texture2D>("Others/startButton");
                    _exitButton = new Button(buttontexture)
                    {
                        Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 + 50),
                        Text = "Ok",
                        Layer = 0.3f,

                    };
                    break;
                case 2:
                    buttontexture = content.Load<Texture2D>("Others/startButton");
                    _exitButton = new Button(buttontexture)
                    {
                        Position = new Vector2(Globals.ScreenWidth / 2 + 70, Globals.ScreenHeight / 2 + 50),
                        Text = "Cancel",
                        Layer = 0.3f,

                    };
                    buttontexture2 = content.Load<Texture2D>("Others/startButton");
                    _exitButton2 = new Button(buttontexture)
                    {
                        Position = new Vector2(Globals.ScreenWidth / 2 - 70, Globals.ScreenHeight / 2 + 50),
                        Text = "Ok",
                        Layer = 0.3f,

                    };
                    break;
            }

        }

        public Window(Texture2D texture, ContentManager content, int number_of_buttons)
        {
            this.texture = texture;
            this.content = content;
            this.number_of_buttons = number_of_buttons;
            buttonUpdate();
        }

        public void Update()
        {
            _exitButton.Update();
            if (number_of_buttons == 2)
            {
                _exitButton2.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var color = Color.White;
            spriteBatch.Draw(texture, Position, null, color, 0f, Origin, 1f, SpriteEffects.None, Layer);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (Globals.gameFont.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (Globals.gameFont.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(Globals.gameFont, Text, new Vector2(x, y), Color.Black, 0f, new Vector2(0, 0), 1f,
                    SpriteEffects.None, Layer + 0.01f);
            }
            _exitButton.Draw(spriteBatch);
            if (number_of_buttons == 2)
            {
                _exitButton2.Draw(spriteBatch);
            }
        }

        #endregion
    }
}