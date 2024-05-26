using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PO_game.Src.Controls
{

    public class Button
    {
       #region Fields
        private MouseState currentMouse;
        private MouseState prevMouse;
        private SpriteFont font;
        private bool isHovering;
        private Texture2D texture;
        #endregion

        #region Properties
        public EventHandler leftClick;
        public EventHandler rightClick;
        public bool Clicked { get; private set; }
        public float Layer { get; set; }
        public Vector2 Origin 
        { get
            {
                return new Vector2(texture.Width/2, texture.Height/2);
            }
        }
        public Vector2 Position { get; set; }

        public Rectangle Rectangle{
            get
            {
                return new Rectangle((int)Position.X - (int)Origin.X , (int)Position.Y - (int)Origin.Y, texture.Width, texture.Height);
            }
        }

        public string Text { get; set; }
        #endregion

        #region Methods
        public Button(Texture2D texture , SpriteFont font)
        {
            this.texture = texture;
            this.font = font;
        }
   
        public void Update()
        {
            prevMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = false;

            if(mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;

                if(currentMouse.LeftButton == ButtonState.Released && prevMouse.LeftButton == ButtonState.Pressed)
                {
                    leftClick?.Invoke(this, new EventArgs());
                }
                if(currentMouse.RightButton == ButtonState.Released && prevMouse.RightButton == ButtonState.Pressed)
                {
                    rightClick?.Invoke(this, new EventArgs());
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            var color = Color.Gray;
            
            if (isHovering)
                color = Color.DarkGray;

            spriteBatch.Draw(texture, Position, null, color, 0f, Origin, 1f, SpriteEffects.None, Layer);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(font, Text, new Vector2(x, y), Color.Black, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, Layer + 0.01f);
            }
        }

        #endregion
    }
}