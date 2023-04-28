using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame.Model;

public class HunterModel : ICreature
{
    public CreatureType Type => CreatureType.Hunter;
    public Vector2 Position { get; set; }
    public Vector2 HorizontalShift => new Vector2(5, 0);
    public Vector2 VerticalShift => new Vector2(0, 0);
    public int DamagePower => 10;
    public double AttackPeriod => 0.5;
    public int Health { get; private set; }

    public HunterModel(Vector2 coordinates)
    {
        Position = coordinates;
        Health = 100;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage <= Health ? damage : Health;
    }
}