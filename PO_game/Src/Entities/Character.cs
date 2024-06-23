using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Effects;
using PO_game.Src.Utils;

namespace PO_game.Src.Entities
{
    /// <summary>
    /// <c>Character</c> is a class that represents a character in the game, be it player or NPC.
    /// <para>It allows the creation of characters with custom sprites, health, mana and position.</para>
    /// </summary>
    public class Character
    {
        public Sprite Sprite { get; set; }
        public CharacterState State { get; set; }
        public float maxHealth { get; set; }
        public float health { get; set; }
        public int maxMana { get; set; }
        public int mana { get; set; }
        public int damageReduction = 0;
        public StatusEffects effects{ get; set; }
        public Vector2 TilePosition { get; set; }



        public Character(Sprite sprite, Vector2 tilePosition)
        {
            Sprite = sprite;
            TilePosition = tilePosition;
            Sprite.Position = new Vector2(
                TilePosition.X * Globals.TileSize + Globals.TileSize / 2 + Sprite.Texture.Width % Globals.TileSize,
                TilePosition.Y * Globals.TileSize - Sprite.Texture.Height % Globals.TileSize);
            State = CharacterState.Idle;
            effects = new StatusEffects();
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
        }
        public void ApplyEffect(StatusEffectType effect, int duration)
        {
            effects.ApplyEffect(effect, duration);
        }
        public void RemoveEffect(StatusEffectType effect)
        {
            effects.RemoveEffect(effect);
        }
        public void RemoveAllEffects()
        {
            effects.RemoveAllEffects();
        }
        public void RestoreHealth(int amount)
        {
            health += amount;
            if (health > maxHealth)
                health = maxHealth;
        }
        public void RestoreMana(int amount)
        {
            mana += amount;
            if (mana > maxMana)
                mana = maxMana;
        }
        /// <summary>
        /// <para>Increases the character's damage reduction by the specified amount.</para>
        /// </summary>
        /// <param name="amount"></param>
        public void Fortify(int amount)
        {
            damageReduction += amount;
        }   
        /// <summary>
        /// <para>Decreases the character's damage reduction by the specified amount and sets it to zero if it would fall below.</para>
        /// </summary>
        public void DeFortify(int amount)
        {
            damageReduction -= amount;
            if (damageReduction < 0)
                damageReduction = 0;   
        }

        public virtual void Update(GameTime gameTime) {}

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(Sprite.Position.X - Sprite.Texture.Width / 2f, Sprite.Position.Y - Sprite.Texture.Height / 2f + Sprite.Texture.Height / 2f);
            spriteBatch.Draw(Sprite.Texture, position, Color.White);
        }

    }
}
