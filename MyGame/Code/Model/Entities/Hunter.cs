using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MyGame.Code.Model.Entities;

public sealed class Hunter : BaseCreature
{
    public override CreatureType Type => CreatureType.Hunter;
    public override int DamagePower => 10;

    protected override Vector2 HorizontalShift => new(5, 0);
    protected override Vector2 VerticalShift => new(0, 0);
    protected override int Health { get; set; }
    protected override double AttackPeriod => 0.5;

    private readonly Goose player;
    private double timer;

    public Hunter(Goose player, Vector2 position, ContentManager contentManager, Vector2 min, Vector2 max) :
        base(position, contentManager, ResourceNames.Hunter, min, max)
    {
        Health = 100;
        this.player = player;
    }

    public override void Update(GameTime gameTime)
    {
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
        if (gameTime.TotalGameTime.TotalSeconds - timer > AttackPeriod)
        {
            BulletsManager.AddBullet(new Bullet(this, contentManager, player.Position.ToPoint()));
            timer = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}