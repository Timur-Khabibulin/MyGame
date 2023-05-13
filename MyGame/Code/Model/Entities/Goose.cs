using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Point = System.Drawing.Point;

namespace MyGame.Code.Model.Entities;

public sealed class Goose : BaseCreature
{
    public override CreatureType Type => CreatureType.Goose;
    public override int DamagePower => 10;

    protected override double AttackPeriod => 0.1;
    protected override Vector2 HorizontalShift => new(5, 0);
    protected override Vector2 VerticalShift => new(0, 5);
    protected override int Health { get; set; }

    private double timer;

    public Goose(Vector2 position, ContentManager contentManager, Vector2 min, Vector2 max) :
        base(position, contentManager, ResourceNames.Goose, min, max)
    {
        Health = 100;
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

    public void Attack(GameTime gameTime, Vector2 direction)
    {
        if (gameTime.TotalGameTime.TotalSeconds - timer > AttackPeriod)
        {
            BulletsManager.AddBullet(new Bullet(this, contentManager, direction));
            timer = gameTime.TotalGameTime.TotalSeconds;
        }
    }

    public void MoveUp() => TryMove(Position - VerticalShift);

    public void MoveDown() => TryMove(Position + VerticalShift);

    public void MoveRight() => TryMove(Position + HorizontalShift);

    public void MoveLeft() => TryMove(Position - HorizontalShift);

    protected override void UpdateAll(GameTime gameTime)
    {
        progressBar.Update(Health, Position);
    }

    private void TryMove(Vector2 newPosition)
    {
        if (!IsDead)
            Position = Vector2.Clamp(newPosition, Min, Max);
    }
}