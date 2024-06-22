using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Entities;
using PO_game.Src.Maps;
using PO_game.Src.Items;
using PO_game.Src.Utils;
using System;

namespace PO_game.Src.Screens;
/// <summary>
/// <c>BattleScreen</c> is a class that represents the battle screen in the game.
/// <para> It allows the <c>Player</c> to fight an <c>Enemy</c> in a classical turn-based style. </para>
/// </summary>
public class BattleScreen : Screen
{
    private Texture2D _playerTexture;
    private Texture2D _enemyTexture;
    private Texture2D _buttonTexture;
    private Button _attackButton;
    private Button _button2;
    private Button _button3;
    private Button _fleeButton;
    private int buttonSpacing = 20;
    private Player _player;
    private Enemy _enemy;
    private bool _playerTurn = true;
    private Health_bar _playerHealthBar;
    private Health_bar _enemyHealthBar;
    

    public BattleScreen (ContentManager content, Player player, Enemy enemy) : base(content)
    {
        _player = player;
        _enemy = enemy;
    }

    /// <summary>
    /// <c>LoadContent</c> is a method that loads the content of the battle screen.
    /// <para>It loads the textures of the player, enemy, and buttons, and creates the health bars for the player and the enemy.</para>
    /// </summary>
    public override void LoadContent()
    {
        _playerHealthBar = new Health_bar(content, new(Globals.ScreenWidth / 10, Globals.ScreenHeight / 7), _player.maxHealth);
        _enemyHealthBar = new Health_bar(content, new(Globals.ScreenWidth / 1.37f, Globals.ScreenHeight / 7), _enemy.maxHealth);
        _playerTexture = _player.Sprite.Texture;
        _enemyTexture = _enemy.Sprite.Texture;
        _buttonTexture = content.Load<Texture2D>("Others/startButton");

        _attackButton = new Button(_buttonTexture)
        {
            Position = new Vector2(Globals.ScreenWidth / 3, Globals.ScreenHeight -
                                                                  buttonSpacing - _buttonTexture.Height - 40),
            Text = "Attack",
            leftClick = new EventHandler(AttackClick),
            Layer = 0.3f
        };

        _button2 = new Button(_buttonTexture)
        {
            Position = new Vector2((float)(Globals.ScreenWidth / 1.5), Globals.ScreenHeight -
                                                                     buttonSpacing - _buttonTexture.Height - 40),
            Text = "2",
            leftClick = new EventHandler(Button2_Click),
            Layer = 0.3f
        };

        _button3 = new Button(_buttonTexture)
        {
            Position = new Vector2(Globals.ScreenWidth / 3, Globals.ScreenHeight
                                                                    + buttonSpacing - _buttonTexture.Height),
            Text = "3",
            leftClick = new EventHandler(Button3_Click),
            Layer = 0.3f
        };

        var fleeText = "";
        switch(_enemy.isAgressive)
        {    
            case true:
                fleeText = "You can't flee from this enemy";
                break;
            case false:
                fleeText = "Flee";
                break;
        }
        _fleeButton = new Button(_buttonTexture)
        {
            Position = new Vector2((float)(Globals.ScreenWidth / 1.5), Globals.ScreenHeight
                                                                    + buttonSpacing - _buttonTexture.Height),
            Text = fleeText,
            leftClick = new EventHandler(FleeClick),
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

    public void FleeClick(object sender, EventArgs e)
    {
        switch (_enemy.isAgressive)
        {
            case true:
                break;
            case false:
                ScreenManager.Instance.RemoveScreen();
                break;
        }
    }
    /// <summary>
    /// <c>RollItemDrop</c> is a method that rolls a chance for an item to drop.
    /// <para> Looks up the global drop rates for rarities and returns a boolean value.</para>
    /// </summary>
    /// <param name="rarity"> The rarity of the item from the monsters loot table.</param>
    /// <returns></returns>
    private bool RollItemDrop(ItemRarity rarity)
    {
        Random random = new Random();
        int roll = random.Next(0, 100);
        if (roll <= Globals.dropChance[rarity])
            return true;
        return false;
    }
    public override void Update(GameTime gameTime)
    {
        if (_playerTurn)
        {
            _attackButton.Update();
            _button2.Update();
            _button3.Update();
            _fleeButton.Update();
        }
        else
        {
            _playerTurn = true;
            _enemy.Attack(_player);
            Console.WriteLine("Enemy attacked " + _player.health + " health left");
        }
        _enemyHealthBar.Update(_enemy.health);
        _playerHealthBar.Update(_player.health);
        if (_player.health <= 0)
        {
            ScreenManager.Instance.RemoveScreen();
        }
        else if (_enemy.health <= 0)
        {
            foreach (Item item in _enemy.loot)
            {
                if (RollItemDrop(item.Rarity))
                    _player.inventory.AddItem(item);
            }
            MapManager.Instance.GetCurrentMap().RemoveEnemy(_enemy);
            ScreenManager.Instance.RemoveScreen();
        }
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        spriteBatch.Draw(_playerTexture,
            new Rectangle(Globals.ScreenWidth / 8, Globals.ScreenHeight / 4, 100, 200),
            Color.White);
        spriteBatch.Draw(_enemyTexture,
            new Rectangle((int)(Globals.ScreenWidth / 1.33), Globals.ScreenHeight / 4, 100, 200),
            Color.White);
        _attackButton.Draw(spriteBatch);
        _button2.Draw(spriteBatch);
        _button3.Draw(spriteBatch);
        _fleeButton.Draw(spriteBatch);
        _playerHealthBar.Draw(spriteBatch);
        _enemyHealthBar.Draw(spriteBatch);
        spriteBatch.End();
    }
}
