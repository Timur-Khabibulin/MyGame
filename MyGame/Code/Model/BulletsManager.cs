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
        => bullets.Add(bullet);

    public static void RemoveAll()
        => bullets.Clear();

    public static void Update(IReadOnlyCollection<BaseCreature> creatures)
    {
        foreach (var bullet in bullets)
        {
            if (bullet.IsActive)
                FindCandidateToDamage(creatures, bullet);
        }
    }

    private static void FindCandidateToDamage(IReadOnlyCollection<BaseCreature> creatures, Bullet bullet)
    {
        foreach (var creature in creatures)
        {
            if (bullet.Parent != CreatureType.Goose && creature.Type == CreatureType.Goose ||
                bullet.Parent == CreatureType.Goose && creature.Type != CreatureType.Goose)
            {
                if (!creature.IsDead && creature.TakeDamage(bullet))
                    bullet.DeActivate();
            }
        }
    }
}