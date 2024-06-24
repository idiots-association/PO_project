using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Utils;

namespace PO_game.Src.Controls
{
    /// <summary>
    /// <c>Window</c> is a class that represents a window in the game.
    /// <para>It allows the creation of windows with custom text, texture and buttons.</para>
    /// 
    /// </summary>

    public class Window
    {
       
        #region Fields
         /// <summary>
         /// Represents the exit button in the window.
         /// </summary>
         public Button ExitButton;
         /// <summary>
         /// Represents second exit button in the window.
         /// </summary>
         public Button ExitButton2;
        private ContentManager _content;
        private Texture2D _texture;
        private Texture2D _buttontexture;
        private Texture2D _button2Texture;
        private int _numberOfButtons;
        #endregion

        #region Properties
        /// <summary>
        /// Represents the origin of the window.
        /// </summary>
        public Vector2 Origin
        {
            get { return new Vector2(_texture.Width / 2, _texture.Height / 2); }
        }
        
        /// <summary>
        /// Represents the position of the window.
        /// </summary>
        public Vector2 Position { get; set; }
        
        /// <summary>
        /// Represents the rectangle of the window.
        /// </summary>
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _texture.Width,
                    _texture.Height);
            }
        }
        /// <summary>
        /// Text of the window.
        /// </summary>
        public string Text { get; set; }

        #endregion

        #region Methods
        
        /// <summary>
        /// A method that creates a window with a message and buttons.
        /// </summary>
        public void ButtonUpdate()
        {
            _buttontexture = _content.Load<Texture2D>("Others/cancelButton");
            _button2Texture = _content.Load<Texture2D>("Others/okButton");
            switch (_numberOfButtons)
            {
                case 1:
                    ExitButton = new Button(_button2Texture)
                    {
                        Position = new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 + 50),
                        Layer = 0.3f,
                    };
                    break;
                case 2:
                    ExitButton = new Button(_buttontexture)
                    {
                        Position = new Vector2(Globals.ScreenWidth / 2 + 70, Globals.ScreenHeight / 2 + 50),
                        Layer = 0.3f,
                    };
                    
                    ExitButton2 = new Button(_button2Texture)
                    {
                        Position = new Vector2(Globals.ScreenWidth / 2 - 70, Globals.ScreenHeight / 2 + 50),
                        Layer = 0.3f,
                    };
                    break;
            }

        }
        /// <summary>
        /// Constructor of the <c>Window</c> class.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="content"></param>
        /// <param name="numberOfButtons"></param>
        public Window(Texture2D texture, ContentManager content, int numberOfButtons)
        {
            _texture = texture;
            _content = content;
            _numberOfButtons = numberOfButtons;
            ButtonUpdate();
        }
        /// <summary>
        /// Update method for the window.
        /// </summary>
        public void Update()
        {
            ExitButton.Update();
            if (_numberOfButtons  == 2)
            {
                ExitButton2.Update();
            }
        }
        /// <summary>
        /// Draw method for the window.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            var color = Color.White;
            spriteBatch.Draw(_texture, Position, null, color, 0f, Origin, 1f, SpriteEffects.None, 0.2f);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (Globals.gameFont.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (Globals.gameFont.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(Globals.gameFont, Text, new Vector2(x, y), Color.Black, 0f, new Vector2(0, 0), 1f,
                    SpriteEffects.None, 0.2f);
            }
            ExitButton.Draw(spriteBatch);
            if (_numberOfButtons == 2)
            {
                ExitButton2.Draw(spriteBatch);
            }
        }

        #endregion
    }
}