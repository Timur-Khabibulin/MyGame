using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MyGame.Code.Model.Entities;

public sealed class Hunter : BaseCreature
{
    public override CreatureType Type => CreatureType.Hunter;
    public override int DamagePower => 1;

    protected override Vector2 HorizontalShift => new(5, 0);
    protected override Vector2 VerticalShift => new(0, 0);
    protected override int Health { get; set; }
    protected override double AttackPeriod => 0.5;

    private readonly double movePeriod = 3;
    private readonly Goose player;
    private double shootTimer;
    private double moveTimer;
    private Random random;

    public Hunter(Goose player, Vector2 position, ContentManager contentManager, Vector2 min, Vector2 max) :
        base(position, contentManager, ResourceNames.Hunter, min, max)
    {
        Health = 100;
        this.player = player;
        random = new Random();
    }

    protected override void UpdateAll(GameTime gameTime)
    {
        Move(gameTime);
        MakeShoot(gameTime);
        progressBar.Update(Health, Position);
    }

    public override bool TakeDamage(Bullet bullet)
    {
        if (IsCollided(bullet.ViewArea))
        {
            Health -= bullet.Damage <= Health ? bullet.Damage : Health;
            return true;
        }

        return false;
    }

    private void MakeShoot(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.TotalSeconds - shootTimer > AttackPeriod)
        {
            BulletsManager.AddBullet(new Bullet(this, contentManager, player.Position.ToPoint()));
            shootTimer = gameTime.TotalGameTime.TotalSeconds;
        }
    }

    private void Move(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.TotalSeconds - moveTimer > movePeriod)
        {
            var x = random.Next((int)Min.X, (int)Max.X);
            Position = new Vector2(x, Position.Y);
            moveTimer = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}