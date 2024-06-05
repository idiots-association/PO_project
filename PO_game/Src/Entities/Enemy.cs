using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Items;



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

            switch (enemyType)
            {
                case EnemyType.Goblin:
                    enemyTexture = content.Load<Texture2D>("Sprites/goblin");
                    break;
                case EnemyType.Orc:
                    enemyTexture = content.Load<Texture2D>("Sprites/orc");
                    break;
                case EnemyType.Troll:
                    enemyTexture = content.Load<Texture2D>("Sprites/troll");
                    break;
                default:
                    return null;
            }

            return new Enemy(new Sprite(enemyTexture), tilePosition, (int)enemyType * 5 + 20);
        }

    }

    public class Enemy : Character
    {
        public Weapon weapon { get; set; }
        public bool IsDead { get; set; }
        public Enemy(Sprite sprite, Vector2 tilePosition, int maxHealth) : base(sprite, tilePosition)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;
            weapon = new Weapon(null, "Some weapon", "Enemy weapon", "Common", 1, 5, this);
        }
        public void Attack(Character target)
        {
            weapon.Attack(target);
        }
    } 
}
