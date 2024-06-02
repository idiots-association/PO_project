using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Items;

namespace PO_game.Src.Entities;

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
