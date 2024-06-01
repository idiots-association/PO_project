using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Items;

namespace PO_game.Src;

public class Enemy : Character
{
    public Weapon weapon { get; set; }
    public bool IsDead { get; set; }
    public Enemy(Sprite sprite, int maxHealth) : base(sprite)
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
