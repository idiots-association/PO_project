using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = System.Numerics.Vector2;
using Microsoft.Xna.Framework.Content;

namespace PO_game.Src.Controls;

public class Health_bar
{
    protected readonly Texture2D background;
    protected readonly Texture2D foreground;
    protected readonly Vector2 position;
    protected readonly float maxHealth;
    protected float currentHealth;
    protected Rectangle part;

    public Health_bar(ContentManager content , Vector2 position, float maxHealth)
    {
        this.background = content.Load<Texture2D>("Others/emptybar");
        this.foreground = content.Load<Texture2D>("Others/healthbar");
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
        spriteBatch.Draw(background, position, null, Color.White, 0, Vector2.Zero, 7f, SpriteEffects.None, 0f);
        spriteBatch.Draw(foreground, position, part, Color.White, 0, Vector2.Zero, 7f , SpriteEffects.None, 1f);
    }
    
}