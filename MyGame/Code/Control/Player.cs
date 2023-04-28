using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Model;

namespace MyGame.Code.Control;

public class Player : IControl
{
    public ICreature Entity { get; }
    public Rectangle Rectangle => new((int)Entity.Position.X, (int)Entity.Position.Y, texture.Width, texture.Height);

    private Texture2D texture;
    private int windowWidth;
    private int windowHeight;
    private Vector2 startPosition = new Vector2(50, 10);
    private List<Bullet> bullets;
    private ContentManager contentManager;
    private double timer;


    public Player(ContentManager content, List<Bullet> bullets, int windowWidth, int windowHeight)
    {
        this.windowHeight = windowHeight;
        this.windowWidth = windowWidth;
        Entity = new GooseModel(startPosition);
        texture = content.Load<Texture2D>(ResourceNames.Goose);
        this.bullets = bullets;
        contentManager = content;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Entity.Position, Color.White);
    }

    public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState)
    {
        Act(gameTime, keyboardState, mouseState);
        CheckForBullets();
    }

    private void Act(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState)
    {
        if (keyboardState.IsKeyDown(InputConfig.Up))
            TryMove(-Entity.VerticalShift);

        if (keyboardState.IsKeyDown(InputConfig.Down))
            TryMove(Entity.VerticalShift);

        if (keyboardState.IsKeyDown(InputConfig.Left))
            TryMove(-Entity.HorizontalShift);

        if (keyboardState.IsKeyDown(InputConfig.Right))
            TryMove(Entity.HorizontalShift);

        if (mouseState.LeftButton == ButtonState.Pressed)
            Attack(gameTime, mouseState);
    }

    private void CheckForBullets()
    {
        foreach (var bullet in bullets)
        {
            if (bullet.Parent == CreatureType.Hunter &&
                Rectangle.Intersects(bullet.Rectangle))
            {
                Entity.TakeDamage(bullet.Damage);
                bullet.DeActivate();
            }
        }
    }

    private void TryMove(Vector2 shift)
    {
        var newPosition = Entity.Position + shift;
        if (newPosition.X >= startPosition.X && newPosition.X < windowWidth - 250 &&
            newPosition.Y >= startPosition.Y && newPosition.Y < windowHeight - 400)
            Entity.Position = newPosition;
    }

    private void Attack(GameTime gameTime, MouseState mouseState)
    {
        if (gameTime.TotalGameTime.TotalSeconds - timer > Entity.AttackPeriod)
        {
            bullets.Add(new Bullet(Entity, contentManager, mouseState.Position));
            timer = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}