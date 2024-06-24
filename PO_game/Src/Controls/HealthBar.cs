using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = System.Numerics.Vector2;
using Microsoft.Xna.Framework.Content;

namespace PO_game.Src.Controls;


/// <summary>
/// <c>HealthBar</c> is a class that represents the health bar in the game.
/// </summary>
public class HealthBar
{
    /// <summary>
    /// The background of the health bar.
    /// </summary>
    protected readonly Texture2D Background;
    /// <summary>
    /// The foreground of the health bar.
    /// </summary>
    protected readonly Texture2D Foreground;
    /// <summary>
    /// The position of the health bar.
    /// </summary>
    protected readonly Vector2 Position;
    /// <summary>
    /// The maximum health of the health bar.
    /// </summary>
    protected readonly float MaxHealth;
    /// <summary>
    /// Current health of the health bar.
    /// </summary>
    protected float CurrentHealth;
    /// <summary>
    /// Part of the health bar.
    /// </summary>
    protected Rectangle Part;
    /// <summary>
    /// Constructor of the health bar.
    /// </summary>
    /// <param name="content"></param>
    /// <param name="position"></param>
    /// <param name="maxHealth"></param>
    public HealthBar(ContentManager content , Vector2 position, float maxHealth)
    {
        this.Background = content.Load<Texture2D>("Others/emptybar");
        this.Foreground = content.Load<Texture2D>("Others/healthbar");
        this.Position = position;   
        this.MaxHealth = maxHealth;
        this.CurrentHealth = maxHealth;
        this.Part = new Rectangle(0, 0, Foreground.Width, Foreground.Height);
    }
    /// <summary>
    /// Updates the health bar.
    /// </summary>
    /// <param name="value"></param>
    public void Update(float value)
    {
        CurrentHealth = value;
        Part.Width = (int)(CurrentHealth / MaxHealth * Foreground.Width);
    }
   /// <summary>
   /// Draws the health bar.
   /// </summary>
   /// <param name="spriteBatch"></param>
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Background, Position, null, Color.White, 0, Vector2.Zero, 7f, SpriteEffects.None, 0f);
        spriteBatch.Draw(Foreground, Position, Part, Color.White, 0, Vector2.Zero, 7f , SpriteEffects.None, 1f);
    }
    
}