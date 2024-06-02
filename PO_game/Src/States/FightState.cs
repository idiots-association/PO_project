using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Entities;
using PO_game.Src.Utils;
using System;

namespace PO_game.Src.States;

public class FightingState : State
{
    private Texture2D _playerTexture;
    private Texture2D _buttonTexture;
    private SpriteFont _buttonFont;
    private Button _button1;
    private Button _button2;
    private Button _button3;
    private Button _button4;
    private int buttonSpacing = 20;
    private Player _player;
    private Enemy _enemy;
    private Texture2D _enemyTexture;

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

        _button1 = new Button(_buttonTexture)
        {
            Position = new Vector2(Globals.ScreenWidth / 3,
             Globals.ScreenHeight - buttonSpacing - _buttonTexture.Height - 40),
            Text = "1",
            leftClick = new EventHandler(Button1Click),
            Layer = 0.3f
        };

        _button2 = new Button(_buttonTexture)
        {
            Position = new Vector2((float)(Globals.ScreenWidth / 1.5),
             Globals.ScreenHeight - buttonSpacing - _buttonTexture.Height - 40),
            Text = "2",
            leftClick = new EventHandler(Button2Click),
            Layer = 0.3f
        };

        _button3 = new Button(_buttonTexture)
        {
            Position = new Vector2(Globals.ScreenWidth / 3,
             Globals.ScreenHeight + buttonSpacing - _buttonTexture.Height),
            Text = "3",
            leftClick = new EventHandler(Button3Click),
            Layer = 0.3f
        };

        _button4 = new Button(_buttonTexture)
        {
            Position = new Vector2((float)(Globals.ScreenWidth / 1.5),
             Globals.ScreenHeight + buttonSpacing - _buttonTexture.Height),
            Text = "4",
            leftClick = new EventHandler(Button4Click),
            Layer = 0.3f
        };
    }

    public void Button1Click(object sender, EventArgs e)
    {
        System.Console.WriteLine("Button 1 clicked");
    }

    public void Button2Click(object sender, EventArgs e)
    {
        System.Console.WriteLine("Button 2 clicked");
    }

    public void Button3Click(object sender, EventArgs e)
    {
        System.Console.WriteLine("Button 3 clicked");
    }

    public void Button4Click(object sender, EventArgs e)
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
            new Rectangle(Globals.ScreenWidth / 8, Globals.ScreenHeight / 4, 100, 200),
            Color.White);
        spriteBatch.Draw(_playerTexture,
            new Rectangle((int)(Globals.ScreenWidth / 1.33), Globals.ScreenHeight / 4, 100, 200),
            Color.White);
        _button1.Draw(spriteBatch);
        _button2.Draw(spriteBatch);
        _button3.Draw(spriteBatch);
        _button4.Draw(spriteBatch);
        spriteBatch.End();
    }
}
