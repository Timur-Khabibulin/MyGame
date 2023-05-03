using System.Collections.Generic;
using MyGame.Code.Model.Entities;

namespace MyGame.Code.Model;

public static class BulletsManager
{
    public static IReadOnlyCollection<Bullet> Bullets => bullets;

    private static readonly List<Bullet> bullets;

    static BulletsManager()
    {
        bullets = new List<Bullet>();
    }

    public static void AddBullet(Bullet bullet)
    {
        bullets.Add(bullet);
    }

    public static void Update(List<BaseCreature> creatures)
    {
        foreach (var bullet in bullets)
        {
            if (bullet.IsActive)
                FindCandidateToDamage(creatures, bullet);
        }
    }

    private static void FindCandidateToDamage(List<BaseCreature> creatures, Bullet bullet)
    {
        foreach (var creature in creatures)
        {
            if (bullet.Parent != CreatureType.Goose && creature.Type == CreatureType.Goose ||
                bullet.Parent == CreatureType.Goose && creature.Type != CreatureType.Goose)
            {
                if (creature.TakeDamage(bullet))
                    bullet.DeActivate();
            }
        }
    }
}