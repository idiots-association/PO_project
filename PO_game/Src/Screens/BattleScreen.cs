using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Controls;
using PO_game.Src.Entities;
using PO_game.Src.Maps;
using PO_game.Src.Items;
using PO_game.Src.Utils;
using PO_game.Src.Effects;  
using System;
using System.Linq;
using PO_game.Src.Items.Consumables;

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
    private Button _usePotionButton;
    private Button _OffHandButton;
    private Button _fleeButton;
    private int buttonSpacing = 20;
    public Player player;
    public Enemy enemy;
    public bool playerTurn = true;
    private bool _playerUsedShield = false;
    private Health_bar _playerHealthBar;
    private Health_bar _enemyHealthBar;
    

    public BattleScreen (ContentManager content, Player player, Enemy enemy) : base(content)
    {
        this.player = player;
        this.enemy = enemy;
    }

    /// <summary>
    /// <c>LoadContent</c> is a method that loads the content of the battle screen.
    /// <para>It loads the textures of the player, enemy, and buttons, and creates the health bars for the player and the enemy.</para>
    /// </summary>
    public override void LoadContent()
    {
        _playerHealthBar = new Health_bar(content, new(Globals.ScreenWidth / 10, Globals.ScreenHeight / 7), player.maxHealth);
        _enemyHealthBar = new Health_bar(content, new(Globals.ScreenWidth / 1.37f, Globals.ScreenHeight / 7), enemy.maxHealth);
        _playerTexture = player.Sprite.Texture;
        _enemyTexture = enemy.Sprite.Texture;
        _buttonTexture = content.Load<Texture2D>("Others/startButton");

        _attackButton = new Button(_buttonTexture)
        {
            Position = new Vector2(Globals.ScreenWidth / 3, Globals.ScreenHeight -
                                                                  buttonSpacing - _buttonTexture.Height - 40),
            Text = "Attack",
            leftClick = new EventHandler(AttackClick),
            Layer = 0.3f
        };

        _usePotionButton = new Button(_buttonTexture)
        {
            Position = new Vector2((float)(Globals.ScreenWidth / 1.5), Globals.ScreenHeight -
                                                                     buttonSpacing - _buttonTexture.Height - 40),
            Text = "2",
            leftClick = new EventHandler(UsePotionClick),
            Layer = 0.3f
        };

        _OffHandButton = new Button(_buttonTexture)
        {
            Position = new Vector2(Globals.ScreenWidth / 3, Globals.ScreenHeight
                                                                    + buttonSpacing - _buttonTexture.Height),
            Text = "Off-hand",
            leftClick = new EventHandler(OffHandClick),
            Layer = 0.3f
        };

        var fleeText = "";
        switch(enemy.isAgressive)
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
        player.Attack(enemy);
        Console.WriteLine("Player attacked " + enemy.health + " health left");
        playerTurn = false;
    }

    //will need to change it later, when more potions are added and mana/skill system is implemented
    public void UsePotionClick(object sender, EventArgs e)
    {
        var healthPotionSlot = player.inventory.slots
            .FirstOrDefault(slot => slot.item is HealthPotion healthPotion && healthPotion.Quantity > 0);

        if (healthPotionSlot != null)
        {
            ((HealthPotion)healthPotionSlot.item).Use(player);
            Console.WriteLine("Player used a health potion. Health is now " + player.health);
            playerTurn = false;
            healthPotionSlot.CheckAndRemoveItemIfEmpty();
        }
        else
        {
            Console.WriteLine("Player has no health potions.");
        }
    }
    public void OffHandClick(object sender, EventArgs e)
    {
        if (player.offHand is Shield)
            {
                player.offHand.Use(this);
                _playerUsedShield = true;
                Console.WriteLine("Player used shield");
            }
        else    
            player.offHand.Use(this);
        playerTurn = false;
        Console.WriteLine("Damage reduction: " + player.damageReduction);
    }
    public void FleeClick(object sender, EventArgs e)
    {
        switch (enemy.isAgressive)
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
    /// <summary>
    /// <c>Update</c> is a method that updates the battle screen.
    /// <para>It updates the player and enemy effects, checks if the player or the enemy is dead, updates the health bars and changes the turns.</para>
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        if (playerTurn)
        {
            player.effects.UpdateEffects(this, player);
            if (player.health <= 0)
            {
                ScreenManager.Instance.RemoveScreen();
            }
            if(playerTurn)
            {
                _attackButton.Update();
                _usePotionButton.Update();
                _OffHandButton.Update();
                _fleeButton.Update();
                
            }
        }
        else
        {
            enemy.effects.UpdateEffects(this, enemy);
            if (enemy.health <= 0)
            {
                foreach (Item item in enemy.loot)
                {
                    if (RollItemDrop(item.Rarity))
                        player.inventory.AddItem(item);
                }
                MapManager.Instance.GetCurrentMap().RemoveEnemy(enemy);
                ScreenManager.Instance.RemoveScreen();
            }
            if(!playerTurn)
            {
                playerTurn = true;
                enemy.Attack(player);
                if (_playerUsedShield)
                {
                    Random random = new Random();
                    if (random.Next(0, 100) >= 50)
                        enemy.ApplyEffect(StatusEffectType.Stun, 1);
                    player.damageReduction -= player.offHand.block ;
                    _playerUsedShield = false;
                }
                Console.WriteLine("Enemy attacked " + player.health + " health left");
        }

        _enemyHealthBar.Update(enemy.health);
        _playerHealthBar.Update(player.health);
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
        _usePotionButton.Draw(spriteBatch);
        _OffHandButton.Draw(spriteBatch);
        _fleeButton.Draw(spriteBatch);
        _playerHealthBar.Draw(spriteBatch);
        _enemyHealthBar.Draw(spriteBatch);
        spriteBatch.End();
    }
}
