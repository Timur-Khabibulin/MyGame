﻿using System;
using Microsoft.Xna.Framework;

namespace MyGame.Code.Model.Entities;

public sealed class Hunter : ICreature, ICollidable
{
    public CreatureType Type => CreatureType.Hunter;
    public bool IsDead => Health <= 0;
    public int DamagePower => 1;
    public Vector2 Position { get; private set; }
    public int Health { get; private set; }
    public Rectangle ViewArea => new(Position.ToPoint(), globals.HunterTextureSize);

    private readonly double attackPeriod = 0.5;
    private readonly double movePeriod = 3;
    private readonly ICreature player;
    private readonly Random random;
    private readonly Globals globals;

    private double shootTimer;
    private double moveTimer;
    private Vector2 minPosition;
    private Vector2 maxPosition;

    public Hunter(ICreature player, Globals globals, Vector2 position)
    {
        this.globals = globals;
        this.player = player;
        Health = 100;
        random = new Random();
        minPosition = new Vector2(globals.Resolution.X * 0.1f, 0);
        maxPosition = new Vector2(globals.Resolution.X * 0.9f, globals.Resolution.Y);
        Position = Vector2.Clamp(position, minPosition, maxPosition);
    }

    public void Act(GameTime gameTime)
    {
        Move(gameTime);
        MakeShoot(gameTime);
    }

    public bool TakeDamage(Bullet bullet)
    {
        if (IsCollided(bullet.ViewArea))
        {
            Health -= bullet.Damage <= Health ? bullet.Damage : Health;
            return true;
        }

        return false;
    }

    public bool IsCollided(Rectangle anotherViewArea)
        => ViewArea.Intersects(anotherViewArea);

    private void MakeShoot(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.TotalSeconds - shootTimer > attackPeriod)
        {
            BulletsManager.AddBullet(new Bullet(globals, this, player.Position));
            shootTimer = gameTime.TotalGameTime.TotalSeconds;
        }
    }

    private void Move(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.TotalSeconds - moveTimer > movePeriod)
        {
            var x = random.Next((int)minPosition.X, (int)maxPosition.X);
            Position = new Vector2(x, Position.Y);
            moveTimer = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}