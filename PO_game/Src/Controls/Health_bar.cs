using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = System.Numerics.Vector2;

namespace PO_game.Src.Controls;

public class Health_bar
{
    protected readonly Texture2D background;
    protected readonly Texture2D foreground;
    protected readonly Vector2 position;
    protected readonly float maxHealth;
    protected float currentHealth;
    protected Rectangle part;
    
    public Health_bar(Texture2D background, Texture2D foreground, Vector2 position, float maxHealth)
    {
        this.background = background;
        this.foreground = foreground;
        this.position = position;
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.part = new Rectangle(0, 0, foreground.Width, foreground.Height);
    }
    
    public void Update(float value)
    {
        currentHealth = value;
        part.Width = (int)(currentHealth / maxHealth * foreground.Width);
    }
   
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background, position, null, Color.White, 0, Vector2.Zero, 5f, SpriteEffects.None, 0f);
        spriteBatch.Draw(foreground, position, part, Color.White, 0, Vector2.Zero, 5f , SpriteEffects.None, 1f);
    }
    
}