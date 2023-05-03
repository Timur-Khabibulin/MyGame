using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Code.Model.Entities;

namespace MyGame.Code.Model;

public abstract class BaseCreature : ISprite
{
    public Texture2D Texture { get; }
    public Rectangle ViewArea => new((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
    public Vector2 Position { get; protected set; }
    public Vector2 Min { get; }
    public Vector2 Max { get; }
    public abstract CreatureType Type { get; }
    public abstract int DamagePower { get; }

    protected abstract Vector2 HorizontalShift { get; }
    protected abstract Vector2 VerticalShift { get; }
    protected abstract int Health { get; set; }
    protected abstract double AttackPeriod { get; }

    protected readonly ContentManager contentManager;

    protected BaseCreature(Vector2 position, ContentManager contentManager, string resourceName, Vector2 min,
        Vector2 max)
    {
        this.contentManager = contentManager;
        Min = min;
        Max = max;
        Position = Vector2.Clamp(position, min, max);
        Texture = contentManager.Load<Texture2D>(resourceName);
    }

    protected bool IsCollided(Rectangle anotherViewArea) => ViewArea.Intersects(anotherViewArea);

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }

    public abstract void Update(GameTime gameTime);

    public abstract bool TakeDamage(Bullet bullet);
}