using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

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

    public override void Update(GameTime gameTime)
    {
        Move();
        Attack(gameTime);
        progressBar.Update(Health,Position);
    }

    private void Move()
    {
        var keyboardState = Keyboard.GetState();
        var newPosition = Position;

        if (keyboardState.IsKeyDown(InputConfig.Up))
            newPosition -= VerticalShift;

        if (keyboardState.IsKeyDown(InputConfig.Down))
            newPosition += VerticalShift;

        if (keyboardState.IsKeyDown(InputConfig.Left))
            newPosition -= HorizontalShift;

        if (keyboardState.IsKeyDown(InputConfig.Right))
            newPosition += HorizontalShift;

        Position = Vector2.Clamp(newPosition, Min, Max);
    }

    private void Attack(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed)
            MakeShoot(gameTime, mouseState);
    }

    private void MakeShoot(GameTime gameTime, MouseState mouseState)
    {
        if (gameTime.TotalGameTime.TotalSeconds - timer > AttackPeriod)
        {
            BulletsManager.AddBullet(new Bullet(this, contentManager, mouseState.Position));
            timer = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}