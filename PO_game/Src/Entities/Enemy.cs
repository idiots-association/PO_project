using System;
using System.IO;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PO_game.Src.Items;
using PO_game.Src.Screens;
using PO_game.Src.Utils;




namespace PO_game.Src.Entities
{

    public enum EnemyType
    {
        Goblin,
        Orc,
        Troll
    }

    public static class EnemyFactory
    {
        public static Enemy CreateEnemy(EnemyType enemyType, Vector2 tilePosition, ContentManager content)
        {
            Texture2D enemyTexture;
            Texture2D weaponTexture;
            Weapon weapon;

            switch (enemyType)
            {
                case EnemyType.Goblin:
                    enemyTexture = content.Load<Texture2D>("Sprites/goblin");
                    weaponTexture = content.Load<Texture2D>("Items/dagger");
                    weapon = new Weapon(weaponTexture, "Goblin Dagger", "A crude dagger made and used by common goblins.", "Common",1,4);
                    return new Enemy(new Sprite(enemyTexture), tilePosition, 20, weapon, false);
                case EnemyType.Orc:
                    enemyTexture = content.Load<Texture2D>("Sprites/orc");
                    weaponTexture = content.Load<Texture2D>("Items/mace");
                    weapon = new Weapon(weaponTexture, "Orcish Mace", "A simple mace used by the most common of orcs", "Common",4,5);
                    return new Enemy(new Sprite(enemyTexture), tilePosition, 45, weapon, false);
                case EnemyType.Troll:
                    enemyTexture = content.Load<Texture2D>("Sprites/troll");
                    weaponTexture = content.Load<Texture2D>("Items/mace"); // need to add a club texture
                    weapon = new Weapon(weaponTexture, "Troll Club", "A large club used by trolls to crush their enemies", "Uncommon",6,8);
                    return new Enemy(new Sprite(enemyTexture), tilePosition, 70, weapon, false);
                default:
                    return null;
            }
        }
    }

    public class Enemy : Character
    {
        public Weapon weapon { get; set; }
        public bool isDead { get; set; }
        public bool isAgressive { get; set; }

        public Enemy(Sprite sprite, Vector2 tilePosition, int maxHealth, Weapon weapon, bool isAgressive) : base(sprite, tilePosition)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
            this.weapon = weapon;
            this.isAgressive = isAgressive;
        }
        private void CheckAgression(ContentManager content, Player player, InputController inputController)
        {       
             
            if (Vector2.Distance(player.TilePosition, TilePosition) < 2 && player.State == CharacterState.Idle)
                {
                    if (isAgressive)
                    {
                        ScreenManager.Instance.AddScreen(new BattleScreen (content, player, this));
                    } 
                    else
                    {// if there is more than one enemy in the radius, i dont know what will happen, will need to fix it somehow
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
            weapon.Attack(target);
        }
    } 
}
