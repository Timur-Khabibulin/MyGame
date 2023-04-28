using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Model;

public class Bullet
{
    public int Damage { get; }
    public Vector2 Position { get; private set; }
    public CreatureType Parent { get; }
    public Texture2D Texture { get; }
    public bool IsActive { get; private set; }
    public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

    private double rotation;
    private float velocity = 10;

    public Bullet(ICreature parent, ContentManager content, Point mousePosition)
    {
        IsActive = true;
        Position = parent.Position;
        Damage = parent.DamagePower;
        Parent = parent.Type;
        rotation = Math.Atan2(mousePosition.Y - Position.Y, mousePosition.X - Position.X);
        Texture = content.Load<Texture2D>(ResourceNames.Bullet);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
            spriteBatch.Draw(Texture, Position, Color.White);
    }

    public void Update()
    {
        if (IsActive)
            Position += new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * velocity;
    }

    public void DeActivate() => IsActive = false;
}