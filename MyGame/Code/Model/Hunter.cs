using Microsoft.Xna.Framework;

namespace MyGame.Model;

public class Hunter : ICreature
{
    public string Name => "Hunter";
    public int DamagePower => 10;
    public Point Coordinates { get; }
    public int Health { get; private set; }


    public Hunter(Point coordinates)
    {
        Coordinates = coordinates;
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