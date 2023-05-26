using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using MyGame.Code;
using MyGame.Code.Model;
using MyGame.Code.Model.Entities;

namespace MyGameTests.Code.Model;

public class BulletsManagerTests
{
    private Globals globals = new(new Point(1920, 1080))
    {
        PlayerTextureSize = new Point(50, 50),
        HunterTextureSize = new Point(50, 50),
        BulletTextureSize = new Point(10, 10)
    };

    private readonly Vector2 goosePostion = new(200, 200);

    [Test]
    public void AddBullet()
    {
        var bullet = GetBullet();
        BulletsManager.AddBullet(bullet);

        Assert.That(BulletsManager.Bullets.Last(), Is.EqualTo(bullet));
    }

    [Test]
    public void RemoveAllBullets()
    {
        BulletsManager.AddBullet(GetBullet());
        BulletsManager.RemoveAll();

        Assert.That(BulletsManager.Bullets.Count, Is.EqualTo(0));
    }

    [Test]
    public void ShotsReachTheVictim()
    {
        var goose = new Goose(globals, goosePostion);
        var hunter = new Hunter(goose, globals, goosePostion,
            0, 0, 10, 100);

        var creatures = new List<ICreature>() { goose, hunter };

        for (int i = 0; i < 100; i++)
            hunter.MakeShoot();

        BulletsManager.Update(creatures);

        Assert.IsTrue(goose.IsDead);
    }

    private Bullet GetBullet()
        => new(globals,
            new Goose(globals, new Vector2(100, 100)),
            new Vector2(1, 1));
}