using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MyGame.Code;
using MyGame.Code.Model.Entities;

namespace MyGameTests.Code.Model.Entities;

public class GooseTest
{
    private Globals globals = new(new Point(1920, 1080))
    {
        PlayerTextureSize = new Point(50, 50),
        HunterTextureSize = new Point(50, 50),
        BulletTextureSize = new Point(10, 10)
    };

    private readonly Vector2 goosePostion = new(200, 200);

    [Test]
    public void GooseTakesDamage()
    {
        var damagedGoose = GooseTryTakeDamage(goosePostion,
            new Vector2(200, 200),
            50);

        Assert.That(damagedGoose.Health, Is.EqualTo(50));
    }

    [Test]
    public void GooseTakesDamageAndDies()
    {
        var damagedGoose = GooseTryTakeDamage(goosePostion,
            new Vector2(200, 200),
            100);

        Assert.That(damagedGoose.Health, Is.EqualTo(0));
        Assert.True(damagedGoose.IsDead);
    }

    [Test]
    public void GooseIncreasesScore()
    {
        var goose = new Goose(globals, goosePostion);
        goose.IncreaseScore(50);
        Assert.That(goose.Score, Is.EqualTo(50));
    }

    [Test]
    [TestCase("MoveUp")]
    [TestCase("MoveDown")]
    [TestCase("MoveRight")]
    [TestCase("MoveLeft")]
    public void GooseMoves(string moveMethodName)
    {
        var goose = new Goose(globals, goosePostion);
        typeof(Goose).GetMethod(moveMethodName)
            ?.Invoke(goose, Array.Empty<Object>());

        Assert.That(goose.Position, Is.Not.EqualTo(goosePostion));
    }

    private Goose GooseTryTakeDamage(Vector2 goosePos, Vector2 hunterPos, int hunterDamagePower)
    {
        var goose = new Goose(globals, goosePos);
        var hunter = new Hunter(goose, globals, hunterPos,
            0, 0, hunterDamagePower, 100);
        var bullet = new Bullet(globals, hunter, goose.Position);

        goose.TakeDamage(bullet);
        return goose;
    }
}