using Microsoft.Xna.Framework;

namespace MyGame.Model;

public interface ICreature
{
    public CreatureType Type { get; }
    public Vector2 Position { get; set; }
    public int Health { get; }

    public Vector2 VerticalShift { get; }

    public Vector2 HorizontalShift { get; }

    public int DamagePower { get; }
    public double AttackPeriod { get; }

    public void TakeDamage(int damage);
}