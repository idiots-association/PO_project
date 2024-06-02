using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Entities;
using PO_game.Src.Items.Consumables;
using PO_game.Src.Utils;
using System;

namespace PO_game.Src.States;

    public class FightingState: State
    {
        private Texture2D _playerTexture;
        private Texture2D _buttonTexture;
        private Button _attackButton;
        private Button _button2;
        private Button _button3;
        private Button _button4;
        private int buttonSpacing = 20;
        private Player _player;
        private Enemy _enemy;
        private bool _playerTurn = true;
        private Texture2D _enemyTexture;
        private HealthPotion _medpot;

        public FightingState(ContentManager content, Player player, Enemy enemy) : base(content)
        {
            _player = player;
            _enemy = enemy;
        }
        public override void LoadContent()
        {
            _playerTexture = _player.Sprite.Texture;
            _enemyTexture = _enemy.Sprite.Texture;
            _buttonTexture = content.Load<Texture2D>("startButton");
            _buttonFont = content.Load<SpriteFont>("Arial"); 
            _medpot = new HealthPotion(content.Load<Texture2D>("medium_health_potion"), "Health Potion", "Heals 50 health", "Common", 50, 1, _player);
            
            _attackButton = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth/3 , GlobalSettings.ScreenHeight  - 
                                                                      buttonSpacing - _buttonTexture.Height - 40),
                Text = "Attack",
                leftClick = new EventHandler(AttackClick),
                Layer = 0.3f
            };
            
            _button2 = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((float)(GlobalSettings.ScreenWidth / 1.5) , GlobalSettings.ScreenHeight  - 
                                                                         buttonSpacing - _buttonTexture.Height - 40),
                Text = "2",
                leftClick = new EventHandler(Button2_Click),
                Layer = 0.3f
            };
            
            _button3 = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2(GlobalSettings.ScreenWidth / 3 , GlobalSettings.ScreenHeight  
                                                                        + buttonSpacing - _buttonTexture.Height),
                Text = "3",
                leftClick = new EventHandler(Button3_Click),
                Layer = 0.3f
            };
            
            _button4 = new Button(_buttonTexture, _buttonFont)
            {
                Position = new Vector2((float)(GlobalSettings.ScreenWidth / 1.5) , GlobalSettings.ScreenHeight  
                                                                        + buttonSpacing - _buttonTexture.Height),
                Text = "4",
                leftClick = new EventHandler(Button4_Click),
                Layer = 0.3f
            };
        }
        
        public void AttackClick(object sender, EventArgs e)
        {
            _player.Attack(_enemy);
            Console.WriteLine("Player attacked " + _enemy.health + " health left");
            _playerTurn = false;
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
            if (_playerTurn)
            {
                _attackButton.Update();
                _button2.Update();
                _button3.Update();
                _button4.Update();
            }
            else
            {
                _playerTurn = true;
                _enemy.Attack(_player);
                Console.WriteLine("Enemy attacked " + _player.health + " health left");
            }
            if (_player.health <= 0)
            {
                Console.WriteLine("Player died");
                StateManager.Instance.RemoveState();
            }
            else if (_enemy.health <= 0)
            {
                Console.WriteLine("Enemy died");
                _player.inventory.AddItem(_medpot);
                _enemy.IsDead = true;
                StateManager.Instance.RemoveState();
            }
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_playerTexture,
                new Rectangle(GlobalSettings.ScreenWidth / 8, GlobalSettings.ScreenHeight / 4, 100, 200),
                Color.White);
            spriteBatch.Draw(_playerTexture,
                new Rectangle((int)(GlobalSettings.ScreenWidth / 1.33), GlobalSettings.ScreenHeight / 4, 100, 200),
                Color.White);
            _attackButton.Draw(spriteBatch);
            _button2.Draw(spriteBatch);
            _button3.Draw(spriteBatch);
            _button4.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
