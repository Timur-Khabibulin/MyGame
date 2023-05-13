using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Code.Model.Entities;

public class Bullet
{
    public int Damage { get; }
    public Rectangle ViewArea => new((int)position.X, (int)position.Y, texture.Width, texture.Height);
    public CreatureType Parent { get; }
    public bool IsActive { get; private set; }

    private readonly float velocity = 10;
    private readonly Texture2D texture;
    private readonly double rotation;
    private Vector2 position;

    public Bullet(BaseCreature parent, ContentManager content, Vector2 mousePosition)
    {
        IsActive = true;
        position = parent.Position;
        Damage = parent.DamagePower;
        Parent = parent.Type;
        rotation = Math.Atan2(mousePosition.Y - position.Y, mousePosition.X - position.X);
        texture = content.Load<Texture2D>(ResourceNames.Bullet);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
            spriteBatch.Draw(texture, position, Color.White);
    }

    public void Update(GameTime gameTime)
    {
        if (IsActive)
            position += new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * velocity;
    }

    public void DeActivate() => IsActive = false;
}