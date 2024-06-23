using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PO_game.Src.Entities;
using PO_game.Src.Screens;

namespace PO_game.Src.Effects
{
    public enum StatusEffectType
    {
        Stun,
        RecoveryI,
        Poison
    }
    public enum StatusEffectPositivity
    {
        Positive,
        Negative
    }
    /// <summary>
    /// <c>StatusEffect</c> is a class that represents a status effects in the game.
    /// <para>It allows to apply status effects such as stun or recovery to characters.</para>
    /// </summary>
    public class StatusEffects
    {
        public List<(StatusEffectType,int)> effects { get; set; }
        public int numberOfEffects { get; set; }
        public StatusEffects()
        {
            effects = new List<(StatusEffectType, int)>();
            numberOfEffects = 0;
        }
        public void ApplyEffect(StatusEffectType effect, int duration)
        {
            bool flag = false;
            int i = 0;
            while (i < effects.Count && !flag)
            {
                if (effects[i].Item1 == effect)
                {
                    if (effects[i].Item2 < duration)
                        effects[i] = (effect, duration);
                    flag = true;
                }
                i++;
            }
            if (!flag)
            {
                effects.Add((effect, duration));
                numberOfEffects++;
            }
        }
        public void RemoveEffect(StatusEffectType effect)
        {
            foreach ((StatusEffectType, int) e in effects)
            {
                if (e.Item1 == effect)
                {
                    effects.Remove(e);
                    numberOfEffects--;
                    break;//if there are multiple effects of the same type, only one is removed(but there should not be multiple effects of the same type)
                }
            }
        }
        public void RemoveAllEffects()
        {
            effects.Clear();
            numberOfEffects = 0;
        }
        /// <summary>
        /// <para>Activates all the effects affecting the character and lowers their duration every turn.</para>
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="character"></param>
        public void UpdateEffects(BattleScreen screen, Character character)
        {
            for (int i = 0 ; i < effects.Count ; i++)
            {
                switch (effects[i].Item1)
                {
                    case StatusEffectType.Stun:
                        if (character is Player && screen.playerTurn)
                            screen.playerTurn = !screen.playerTurn;
                        else if (character is Enemy && !screen.playerTurn)
                            screen.playerTurn = !screen.playerTurn;
                        break;
                    case StatusEffectType.RecoveryI:
                        character.RestoreHealth(5);
                        break;
                    case StatusEffectType.Poison:
                        character.TakeDamage(5);
                        break;
                }
                effects[i] = (effects[i].Item1, effects[i].Item2 - 1);
                if (effects[i].Item2 <= 0)
                    RemoveEffect(effects[i].Item1);
            }
        }
    }

}