using Microsoft.Xna.Framework;
using MyGame.Code;
using MyGame.Code.Model;
using MyGame.Code.Model.Entities;

namespace MyGameTests.Code.Model.Entities;

public class HunterTest
{
    private Globals globals = new(new Point(1920, 1080))
    {
        PlayerTextureSize = new Point(50, 50),
        HunterTextureSize = new Point(50, 50),
        BulletTextureSize = new Point(10, 10)
    };

    private readonly Vector2 hunterPostion = new(200, 200);

    [Test]
    public void HunterMakeShoot()
    {
        var hunter = GetDefaultHunter();

        var oldBulletsCount = BulletsManager.Bullets.Count;
        hunter.MakeShoot();
        var newBulletsCount = BulletsManager.Bullets.Count;

        Assert.That(oldBulletsCount, Is.Not.EqualTo(newBulletsCount));
    }

    [Test]
    public void HunterMoves()
    {
        var hunter = GetDefaultHunter();

        hunter.Move();

        Assert.That(hunter.Position, Is.Not.EqualTo(hunterPostion));
    }

    [Test]
    public void HunterDoesntIncreaseScore()
    {
        var hunter = GetDefaultHunter();

        hunter.IncreaseScore(50);
        Assert.That(hunter.Score, Is.EqualTo(0));
    }

    [Test]
    public void HunterTakesDamage()
    {
        var damagedHunter = HunterTryTakeDamage(hunterPostion,
            new Vector2(200, 200),
            50);

        Assert.That(damagedHunter.Health, Is.EqualTo(50));
    }

    [Test]
    public void HunterTakesDamageAndDies()
    {
        var damagedHunter = HunterTryTakeDamage(hunterPostion,
            new Vector2(200, 200),
            100);

        Assert.That(damagedHunter.Health, Is.EqualTo(0));
        Assert.True(damagedHunter.IsDead);
    }

    private Hunter GetDefaultHunter()
        => new Hunter(new Goose(globals, hunterPostion),
            globals, hunterPostion,
            0, 0, 10, 100);

    private Hunter HunterTryTakeDamage(Vector2 goosePos, Vector2 hunterPos, int damagePower)
    {
        var goose = new Goose(globals, goosePos, damagePower);
        var hunter = new Hunter(goose, globals, hunterPos,
            0, 0, damagePower, 100);
        var bullet = new Bullet(globals, goose, goose.Position);

        hunter.TakeDamage(bullet);
        return hunter;
    }
}