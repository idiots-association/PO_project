using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Utils;

namespace PO_game.Src.States;

    public class FightingState: State
    {
        private Texture2D _playerTexture;
        private Texture2D _buttonTexture;
        private SpriteFont _buttonFont;
        private Button _button1;
        private Button _button2;
        private Button _button3;
        private Button _button4;
        private int buttonSpacing = 20;

        public FightingState(ContentManager content): base(content){}
        public override void LoadContent()
        {
            _playerTexture = content.Load<Texture2D>("playerxd");
            _buttonTexture = content.Load<Texture2D>("startButton");
            _buttonFont = content.Load<SpriteFont>("Arial"); 
            
            _button1 = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth/3 , GlobalSettings.ScreenHeight  - 
                                                                      buttonSpacing - _buttonTexture.Height - 40),
                Text = "1",
                Click = new EventHandler(Button1_Click),
                Layer = 0.3f
            };
            
            _button2 = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((float)(GlobalSettings.ScreenWidth / 1.5) , GlobalSettings.ScreenHeight  - 
                                                                         buttonSpacing - _buttonTexture.Height - 40),
                Text = "2",
                Click = new EventHandler(Button2_Click),
                Layer = 0.3f
            };
            
            _button3 = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth / 3 , GlobalSettings.ScreenHeight  
                                                                        + buttonSpacing - _buttonTexture.Height),
                Text = "3",
                Click = new EventHandler(Button3_Click),
                Layer = 0.3f
            };
            
            _button4 = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((float)(GlobalSettings.ScreenWidth / 1.5) , GlobalSettings.ScreenHeight  
                                                                        + buttonSpacing - _buttonTexture.Height),
                Text = "4",
                Click = new EventHandler(Button4_Click),
                Layer = 0.3f
            };
        }
        
        public void Button1_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("Button 1 clicked");
        }
        
        public void Button2_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("Button 2 clicked");
        }
        
        public void Button3_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("Button 3 clicked");
        }
        
        public void Button4_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("Button 4 clicked");
        }
        
        
        
        public override void Update(GameTime gameTime)
        {
            _button1.Update();
            _button2.Update();
            _button3.Update();
            _button4.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_playerTexture,
                new Rectangle(GlobalSettings.ScreenWidth / 8, GlobalSettings.ScreenHeight / 4, 100, 200),
                Color.White);
            _button1.Draw(spriteBatch);
            _button2.Draw(spriteBatch);
            _button3.Draw(spriteBatch);
            _button4.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
