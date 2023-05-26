using System;
using Microsoft.Xna.Framework;

namespace MyGame.Code.Model.Entities;

public class Bullet
{
    public Vector2 Position { get; private set; }
    public int Damage { get; }
    public Rectangle ViewArea => new(Position.ToPoint(), globals.BulletTextureSize);
    public ICreature Parent { get; }
    public bool IsActive { get; private set; }

    private readonly float velocity = 10;
    private readonly double rotation;
    private readonly Globals globals;


    public Bullet(Globals globals, ICreature parent, Vector2 mousePosition)
    {
        this.globals = globals;
        IsActive = true;
        Position = parent.Position;
        Damage = parent.DamagePower;
        Parent = parent;
        rotation = Math.Atan2(mousePosition.Y - Position.Y, mousePosition.X - Position.X);
    }

    public void Move()
    {
        if (IsActive)
            Position += new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * velocity;
    }

    public void DeActivate() => IsActive = false;
}