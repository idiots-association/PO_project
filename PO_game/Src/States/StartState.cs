using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.States;
using PO_game.Src.Controls;

namespace PO_game.Src.States
{
    public class StartState : State
    {
        public Texture2D backgroundTexture;
        public Button startButton;
        public Button exitButton;
        public Button settingsButton;


        public StartState(ContentManager content) : base(content){}
        public override void LoadContent()
        {   
            backgroundTexture = Content.Load<Texture2D>("cool_background");
            /*var startButton = Content.Load<Texture2D>("startButton");
            var buttonFont = Content.Load<SpriteFont>("buttonFont");*/
            
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, new Rectangle(0,0,800,480), Color.White);
        }
    }
}