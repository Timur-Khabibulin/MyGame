using Microsoft.Xna.Framework;

namespace MyGame.Model;

public interface ICreature
{
    public string Name { get; }
    public Point Coordinates { get; }
    public int Health { get; }

    public int DamagePower { get; }

    public void Act();
    public void MakeDamage(ICreature conflictedObject);
    public void TakeDamage(int damage);
}