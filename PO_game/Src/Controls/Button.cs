using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.Utils;
using System;

namespace PO_game.Src.Controls
{
    /// <summary>
    /// <c>Button</c> is a class that represents a button in the game.
    /// <para>It allows the creation of buttons with custom text, texture and different functions.</para>
    /// </summary>
    public class Button
    {
        private MouseState _currentMouse;
        private MouseState _prevMouse;
        private bool _isHovering;
        private Texture2D _texture;

        public EventHandler leftClick;
        public EventHandler rightClick;
        public float Layer { get; set; }
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        
        public float Scale { get; set; } = 1f;
        private Vector2 _origin
        {
            get
            {
                return new Vector2(_texture.Width / 2,_texture.Height / 2);
            }
        }

        
        private Rectangle _rectangle
        {
            get
            {
                return new Rectangle((int)Position.X - (int)_origin.X - (int)Scale * 25, (int)Position.Y - (int)_origin.Y - (int)Scale * 25/4 , 
                    (int)(_texture.Width * Scale), (int)(_texture.Height * Scale));
            }
        }



        public Button(Texture2D texture)
        {
            _texture = texture;
        }

        public void Update()
        {
            _prevMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(_rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _prevMouse.LeftButton == ButtonState.Pressed)
                {
                    leftClick?.Invoke(this, new EventArgs());
                }
                if (_currentMouse.RightButton == ButtonState.Released && _prevMouse.RightButton == ButtonState.Pressed)
                {
                    rightClick?.Invoke(this, new EventArgs());
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            var color = Color.Gray;

            if (_isHovering)
                color = Color.DarkGray;
            
            

            if (!string.IsNullOrEmpty(Text))
            {
                var x = _rectangle.X + (_rectangle.Width/Scale) / 2 - Globals.gameFont.MeasureString(Text).X / 2 + (int)Scale * 25;
                var y = _rectangle.Y + (_rectangle.Height/Scale) / 2 - Globals.gameFont.MeasureString(Text).Y / 2 + (int)Scale * 25/4;
                spriteBatch.Draw(_texture, Position, null, color, 0f, _origin, Scale, SpriteEffects.None, Layer);
                spriteBatch.DrawString(Globals.gameFont, Text, new Vector2(x, y), Color.Black, 0f,
                    new Vector2(0, 0), 1f, SpriteEffects.None, Layer + 0.01f);
            }
            else
            {
                spriteBatch.Draw(_texture, Position, null, color, 0f, _origin, Scale, SpriteEffects.None, Layer);
            }
        }

    }
}