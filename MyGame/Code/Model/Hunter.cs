using Microsoft.Xna.Framework;

namespace MyGame.Model;

public class Hunter : ICreature
{
    public string AssetName { get; }
    public string Name => "Hunter";
    public Vector2 Position { get;  set; }
    public Vector2 HorizontalShift => new Vector2(5, 0);
    public Vector2 VerticalShift => new Vector2(0, 0);
    public int DamagePower => 10;
    public int Health { get; private set; }


    public Hunter(Vector2 coordinates)
    {
        Position = coordinates;
        Health = 100;
    }

    public void Act()
    {
        //throw new System.NotImplementedException();
    }

    public void MakeDamage(ICreature conflictedObject)
    {
        conflictedObject.TakeDamage(DamagePower);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage <= Health ? damage : Health;
    }
}