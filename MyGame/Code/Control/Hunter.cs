using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Model;

namespace MyGame.Code.Control;

public class Hunter : IControl
{
    public ICreature Entity { get; }
    public Rectangle Rectangle => new((int)Entity.Position.X, (int)Entity.Position.Y, texture.Width, texture.Height);

    private Vector2 startPosition = new(1600, 720);
    private Texture2D texture;
    private List<Bullet> bullets;
    private ContentManager contentManager;
    private Player player;
    private double timer;

    public Hunter(ContentManager content, List<Bullet> bullets, Player player)
    {
        Entity = new HunterModel(startPosition);
        texture = content.Load<Texture2D>(ResourceNames.Hunter);
        this.bullets = bullets;
        contentManager = content;
        this.player = player;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Entity.Position, Color.White);
    }

    public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState)
    {
        Attack(gameTime);
        CheckForBullets();
    }

    private void Attack(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.TotalSeconds - timer > Entity.AttackPeriod)
        {
            bullets.Add(new Bullet(Entity, contentManager, player.Entity.Position.ToPoint()));
            timer = gameTime.TotalGameTime.TotalSeconds;
        }
    }

    private void CheckForBullets()
    {
        foreach (var bullet in bullets)
        {
            if (bullet.Parent == CreatureType.Goose &&
                Rectangle.Intersects(bullet.Rectangle))
            {
                Entity.TakeDamage(bullet.Damage);
                bullet.DeActivate();
            }
        }
    }
}