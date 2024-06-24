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
    private Texture2D _attackButtonTexture;
    private Texture2D _potionButtonTexture;
    private Texture2D _offHandButtonTexture;
    private Texture2D _fleeButtonTexture;
    private Texture2D _weaponTexture;
    private Texture2D _backgroundTexture;
    private Button _attackButton;
    private Button _usePotionButton;
    private Button _OffHandButton;
    private Button _fleeButton;
    private int buttonSpacing = 20;
    public Player player;
    public Enemy enemy;
    public bool playerTurn = true;
    public bool playerUsedShield = false;
    private HealthBar _playerHealthBar;
    private HealthBar _enemyHealthBar;
    public string battleText = "";
    public string enemyText = "";


    public BattleScreen(ContentManager content, Player player, Enemy enemy) : base(content)
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
        _playerHealthBar = new HealthBar(content, new(Globals.ScreenWidth / 10, Globals.ScreenHeight / 7), player.maxHealth);
        _enemyHealthBar = new HealthBar(content, new(Globals.ScreenWidth / 1.37f, Globals.ScreenHeight / 7), enemy.maxHealth);
        if (MapManager.Instance.CurrentMap == MapId.DragonPit)
        {
            _backgroundTexture = content.Load<Texture2D>("Others/burnedground");
        }
        else
            _backgroundTexture = content.Load<Texture2D>("Tilesets/trawaxd");
        _playerTexture = player.Sprite.Texture;
        _enemyTexture = enemy.Sprite.Texture;
        _weaponTexture = player.weapon.Texture;
        _attackButtonTexture = content.Load<Texture2D>("Others/attackButton");
        _potionButtonTexture = content.Load<Texture2D>("Others/potionButton");
        _offHandButtonTexture = content.Load<Texture2D>("Others/offHandButton");

        _attackButton = new Button(_attackButtonTexture)
        {
            Position = new Vector2(Globals.ScreenWidth / 3, Globals.ScreenHeight -
                                                                  buttonSpacing - _attackButtonTexture.Height - 80),
            Scale = 4f,
            leftClick = new EventHandler(AttackClick),
            Layer = 0.3f
        };

        _usePotionButton = new Button(_potionButtonTexture)
        {
            Position = new Vector2((float)(Globals.ScreenWidth / 1.5), Globals.ScreenHeight -
                                                                     buttonSpacing - _potionButtonTexture.Height - 80),
            Scale = 4f,
            leftClick = new EventHandler(UsePotionClick),
            Layer = 0.3f
        };

        _OffHandButton = new Button(_offHandButtonTexture)
        {
            Position = new Vector2(Globals.ScreenWidth / 3, Globals.ScreenHeight
                                                                    + buttonSpacing - _offHandButtonTexture.Height - 40),
            Scale = 4f,
            leftClick = new EventHandler(OffHandClick),
            Layer = 0.3f
        };

        switch (enemy.isAgressive)
        {
            case true:
                _fleeButtonTexture = content.Load<Texture2D>("Others/noFleeButton");
                break;
            case false:
                _fleeButtonTexture = content.Load<Texture2D>("Others/fleeButton");
                break;
        }
        _fleeButton = new Button(_fleeButtonTexture)
        {
            Position = new Vector2((float)(Globals.ScreenWidth / 1.5), Globals.ScreenHeight
                                                                    + buttonSpacing - _fleeButtonTexture.Height - 40),
            Scale = 4f,
            leftClick = new EventHandler(FleeClick),
            Layer = 0.3f
        };
    }

    public void AttackClick(object sender, EventArgs e)
    {
        player.Attack(enemy);
        battleText = "You attacked - " + enemy.health + " health left";
        playerTurn = false;
    }

    //will need to change it later, when more potions are added and mana/skill system is implemented
    public void UsePotionClick(object sender, EventArgs e)
    {
        var healthPotionSlot = player.inventory.slots
            .FirstOrDefault(slot => slot.item is HealthPotion healthPotion && healthPotion.Quantity > 0);

        if (healthPotionSlot != null)
        {
            HealthPotion healthPotion = (HealthPotion)healthPotionSlot.item;
            ((HealthPotion)healthPotionSlot.item).Use(player);
            battleText = "Player used a health potion, " + healthPotion.Quantity + " left. Health is now " + player.health;
            playerTurn = false;
            healthPotionSlot.CheckAndRemoveItemIfEmpty();
        }
        else
        {
            battleText = "Player has no health potions.";
        }
    }
    public void OffHandClick(object sender, EventArgs e)
    {
        player.Fortify();
        player.offHand.Use(this);
        playerTurn = false;
        battleText = "Damage reduction: " + player.damageReduction;
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
        _enemyHealthBar.Update(enemy.health);
        _playerHealthBar.Update(player.health);
        if (playerTurn)
        {
            player.effects.UpdateEffects(this, player);
            if (player.health <= 0)
            {
                ScreenManager.Instance.RemoveScreen();
            }
            if (playerTurn)
            {
                _attackButton.Update();
                _usePotionButton.Update();
                _OffHandButton.Update();
                _fleeButton.Update();

            }
            
        }
        else
        {
            enemyText = "";
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
            else if(!playerTurn)
            {

                enemy.Attack(player);
                if (playerUsedShield)
                {
                    Random random = new Random();
                    if (random.Next(0, 100) >= 50)
                    {
                        enemy.ApplyEffect(StatusEffectType.Stun, 1);
                        enemyText = "Enemy is stunned";
                    }
                    playerUsedShield = false;
                }
                enemyText += "\nEnemy attacked - " + player.health + " health left";
                playerTurn = true;
            }
            player.DeFortify();

        }
    } 
    public override void Draw(SpriteBatch spriteBatch)
    {
        
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), null , Color.White,
            0f, Vector2.Zero, 14.2f, SpriteEffects.None, 0.1f);
        spriteBatch.Draw(_playerTexture,
            new Rectangle(Globals.ScreenWidth / 14, Globals.ScreenHeight / 4, 200, 200),
            Color.White);
        spriteBatch.Draw(_enemyTexture,
            new Rectangle((int)(Globals.ScreenWidth / 1.45), Globals.ScreenHeight / 4, 200, 200),
            Color.White);
        spriteBatch.Draw(_weaponTexture,
            new Rectangle((int)(Globals.ScreenWidth / 3.5), Globals.ScreenHeight / 3, 100, 100),
            Color.White);
        _attackButton.Draw(spriteBatch);
        _usePotionButton.Draw(spriteBatch);
        _OffHandButton.Draw(spriteBatch);
        _fleeButton.Draw(spriteBatch);
        _playerHealthBar.Draw(spriteBatch);
        _enemyHealthBar.Draw(spriteBatch);
        if (battleText.Length > 50)
            spriteBatch.DrawString(Globals.gameFont, battleText, new Vector2((int)(Globals.ScreenWidth / 2 - 170)
                , Globals.ScreenHeight / 15), Color.Black, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0.5f);
        else 
            spriteBatch.DrawString(Globals.gameFont, battleText, new Vector2((int)(Globals.ScreenWidth / 2.15 - 100)
                , Globals.ScreenHeight / 15), Color.Black);
        spriteBatch.DrawString(Globals.gameFont, enemyText, new Vector2((int)(Globals.ScreenWidth / 2.15 - 100), Globals.ScreenHeight / 15 + 20), Color.Black);
        spriteBatch.End();
    }
}
