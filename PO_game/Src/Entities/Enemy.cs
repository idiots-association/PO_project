using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.Controls;
using PO_game.Src.Items;
using PO_game.Src.Items.Consumables;
using PO_game.Src.Screens;
using PO_game.Src.Utils;




namespace PO_game.Src.Entities
{
    /// <summary>
    ///  Defines the types of enemies that can be created.
    /// </summary>
    public enum EnemyType
    {
        Wolf,
        Orc,
        Ghost,
        Drake,
        MiniBoss
    }

    /// <summary>
    /// Class <c>EnemyFactory</c> is a class that creates enemies based on the type of enemy requested.
    /// It stores all the pre-set enemy types and their respective textures and weapons.
    /// </summary>
    public static class EnemyFactory
    {
        public static Enemy CreateEnemy(EnemyType enemyType, Vector2 tilePosition, ContentManager content)
        {
            Texture2D enemyTexture;
            Texture2D weaponTexture;
            Weapon weapon;
            List<Item> _loot = new List<Item>();
            Random random = new Random();

            switch (enemyType)
            {
                case EnemyType.Wolf:
                    enemyTexture = content.Load<Texture2D>("Sprites/wolf");
                    weaponTexture = content.Load<Texture2D>("Items/dagger");
                    weapon = new Weapon(weaponTexture, "Goblin Dagger", "A crude dagger made and used by common goblins.", ItemRarity.Common,1,4);
                    _loot.Add(weapon);
                    _loot.Add(PotionFactory.CreatePotion(PotionType.HealthPotion, ItemRarity.Common, 1, content));
                    return new Enemy(new Sprite(enemyTexture), tilePosition, 20, weapon, false, _loot);
                case EnemyType.Orc:
                    enemyTexture = content.Load<Texture2D>("Sprites/orc");
                    weaponTexture = content.Load<Texture2D>("Items/mace");
                    weapon = new Weapon(weaponTexture, "Orcish Mace", "A simple mace used by the most common of orcs", ItemRarity.Common,4,5);
                    _loot.Add(weapon);
                    _loot.Add(PotionFactory.CreatePotion(PotionType.HealthPotion, ItemRarity.Common, random.Next(1,2), content));
                    return new Enemy(new Sprite(enemyTexture), tilePosition, 45, weapon, false, _loot);
                case EnemyType.Ghost:
                    enemyTexture = content.Load<Texture2D>("Sprites/ghost");
                    weaponTexture = content.Load<Texture2D>("Items/mace"); // need to add a club texture
                    weapon = new Weapon(weaponTexture, "Troll Club", "A large club used by trolls to crush their enemies", ItemRarity.Uncommon,6,8);
                    _loot.Add(weapon);
                    _loot.Add(PotionFactory.CreatePotion(PotionType.HealthPotion, ItemRarity.Uncommon, random.Next(1,2), content));
                    return new Enemy(new Sprite(enemyTexture), tilePosition, 70, weapon, false, _loot);
                case EnemyType.Drake:
                    enemyTexture = content.Load<Texture2D>("Sprites/drake");
                    weaponTexture = content.Load<Texture2D>("Items/sword");
                    weapon = new Weapon(weaponTexture, "Sword of the Damned", "A cursed sword that brings death", ItemRarity.Legendary, 10, 15);
                    _loot.Add(weapon);
                    _loot.Add(PotionFactory.CreatePotion(PotionType.HealthPotion, ItemRarity.Uncommon, random.Next(1, 2), content));
                    return new Enemy(new Sprite(enemyTexture), tilePosition, 100, weapon, true, _loot);
                case EnemyType.MiniBoss:
                    enemyTexture = content.Load<Texture2D>("Sprites/miniboss");
                    weaponTexture = content.Load<Texture2D>("Items/mace");
                    weapon = new Weapon(weaponTexture, "Orcish Mace", "A simple mace used by the most common of orcs", ItemRarity.Common, 4, 5);
                    _loot.Add(weapon);
                    _loot.Add(PotionFactory.CreatePotion(PotionType.HealthPotion, ItemRarity.Common, random.Next(1, 2), content));
                    return new Enemy(new Sprite(enemyTexture), tilePosition, 45, weapon, false, _loot);
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// <c>Enemy</c> is a class that represents an enemy character in the game.
    /// <para>It stores all the enemy's attributes, such as health, weapon, and agression.</para>
    /// </summary>
    public class Enemy : Character
    {
        public Weapon weapon { get; set; }
        public bool isAgressive { get; set; }
        public List<Item> loot { get; set; }

        public Enemy(Sprite sprite, Vector2 tilePosition, int maxHealth, Weapon weapon, bool isAgressive, List<Item> loot) : base(sprite, tilePosition)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
            this.weapon = weapon;
            this.isAgressive = isAgressive;
            this.loot = loot;   
        }
        /// <summary>
        /// <para>Checks if the player is in the aggro radius of the enemy and initiates the fight if the criteria are met.</para>
        /// </summary>
        /// <param name="content"></param>
        /// <param name="player"></param>
        /// <param name="inputController"></param>
        private void CheckAgression(ContentManager content, Player player, InputController inputController)
        {       
             
            if (Vector2.Distance(player.TilePosition, TilePosition) < 2 && player.State == CharacterState.Idle)
                {
                    if (isAgressive)
                    {
                        ScreenManager.Instance.AddScreen(new BattleScreen (content, player, this));
                    } 
                    else
                    {// if there is more than one enemy in the radius, i dont know what will happen, will need to fix it somehow later
                        if (inputController.isKeyPressed(Keys.F) && player.health > 0)
                        {
                            ScreenManager.Instance.AddScreen(new BattleScreen(content, player, this));
                        }
                    }  
                }
            }
        public void Update(ContentManager content, Player player, InputController inputController)
        {
            CheckAgression(content, player, inputController);
        }
        public void Attack(Character target)
        {
            int damage = weapon.Attack() - target.damageReduction;
            if (damage < 0)
                damage = 0;
            target.TakeDamage(damage);
        }
    } 
}
