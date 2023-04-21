using Microsoft.Xna.Framework;

namespace MyGame.Model;

public interface ICreature
{
    public string AssetName { get; }
    public string Name { get; }
    public Vector2 Position { get; set; }
    public int Health { get; }

    public Vector2 VerticalShift { get; }

    public Vector2 HorizontalShift { get; }

    public int DamagePower { get; }

    public void Act();
    public void MakeDamage(ICreature conflictedObject);
    public void TakeDamage(int damage);
}