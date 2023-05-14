using Microsoft.Xna.Framework;

namespace MyGame.Code.Model.Entities;

public sealed class Goose : ICreature, ICollidable
{
    public CreatureType Type => CreatureType.Goose;
    public bool IsDead => Health <= 0;
    public int DamagePower => 10;
    public Vector2 Position { get; private set; }
    public int Health { get; private set; }
    public Rectangle ViewArea => new(Position.ToPoint(), globals.PlayerTextureSize);

    private readonly double attackPeriod = 0.1;
    private readonly Vector2 horizontalShift = new(5, 0);
    private readonly Vector2 verticalShift = new(0, 5);
    private readonly Globals globals;

    private double timer;
    private readonly Vector2 minPosition;
    private readonly Vector2 maxPosition;

    public Goose(Globals globals, Vector2 position)
    {
        this.globals = globals;
        Health = 100;
        minPosition = new Vector2(globals.Resolution.X * 0.02f, globals.Resolution.Y * 0.01f);
        maxPosition = new Vector2(globals.Resolution.X * 0.8f, globals.Resolution.Y * 0.5f);
        Position = Vector2.Clamp(position, minPosition, maxPosition);
    }

    public void Act(GameTime gameTime)
    {
    }

    public bool TakeDamage(Bullet bullet)
    {
        if (IsDead) return false;

        if (IsCollided(bullet.ViewArea))
        {
            Health -= bullet.Damage <= Health ? bullet.Damage : Health;
            return true;
        }

        return false;
    }

    public void Attack(GameTime gameTime, Vector2 direction)
    {
        if (!IsDead)
        {
            if (gameTime.TotalGameTime.TotalSeconds - timer > attackPeriod)
            {
                BulletsManager.AddBullet(new Bullet(globals, this, direction));
                timer = gameTime.TotalGameTime.TotalSeconds;
            }
        }
    }

    public bool IsCollided(Rectangle anotherViewArea)
        => ViewArea.Intersects(anotherViewArea);

    public void MoveUp() => TryMove(Position - verticalShift);

    public void MoveDown() => TryMove(Position + verticalShift);

    public void MoveRight() => TryMove(Position + horizontalShift);

    public void MoveLeft() => TryMove(Position - horizontalShift);

    private void TryMove(Vector2 newPosition)
    {
        if (!IsDead)
            Position = Vector2.Clamp(newPosition, minPosition, maxPosition);
    }
}