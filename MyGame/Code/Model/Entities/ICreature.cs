using Microsoft.Xna.Framework;

namespace MyGame.Code.Model.Entities;

public interface ICreature
{
    public CreatureType Type { get; }
    public Vector2 Position { get; }
    public bool IsDead { get; }
    public int Health { get; }
    public int DamagePower { get; }

    public void Act(GameTime gameTime);
    public bool TakeDamage(Bullet bullet);
}