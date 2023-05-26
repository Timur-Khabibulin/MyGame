using Microsoft.Xna.Framework;
using MyGame.Code;
using MyGame.Code.Model.Entities;

namespace MyGameTests.Code.Model.Entities;

public class BulletTest
{
    private Globals globals = new(new Point(1920, 1080));

    [Test]
    public void BulletMovesIfActive()
    {
        var position = new Vector2(100, 100);
        var parent = new Goose(globals, position);
        var bullet = new Bullet(globals, parent, new Vector2(1, 1));

        bullet.Move();

        Assert.That(bullet.Position, Is.Not.EqualTo(position));
    }

    [Test]
    public void BulletDoesntMoveIfDeActiveted()
    {
        var bullet = GetBullet();

        bullet.DeActivate();
        bullet.Move();

        Assert.That(bullet.Position, Is.EqualTo(bullet.Parent.Position));
    }

    private Bullet GetBullet()
        => new(globals,
            new Goose(globals, new Vector2(100, 100)),
            new Vector2(1, 1));
}