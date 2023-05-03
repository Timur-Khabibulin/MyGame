using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Code.Model;
using MyGame.Code.Model.Entities;

namespace MyGame.Code.View;

public class World : IComponent
{
    private readonly Texture2D ground;
    private int width;
    private int height;
    private int groundHeight;

    private readonly List<BaseCreature> creatures;

    public World(ContentManager content, int width, int height)
    {
        ground = content.Load<Texture2D>(ResourceNames.Ground);
        this.width = width;
        this.height = height;
        groundHeight = height / 4;

        creatures = new List<BaseCreature>();
        var player = new Goose(new Vector2(50, 10),
            content,
            new Vector2(50, 10),
            new Vector2(width * 0.6f, height * 0.6f));

        creatures.Add(player);

        creatures.Add(new Hunter(player, new Vector2(1600, 720),
            content,
            new Vector2(0, 0),
            new Vector2(width, height)));
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(ground, new Rectangle(0,
            height - groundHeight,
            width,
            groundHeight), Color.White);

        foreach (var bullet in BulletsManager.Bullets)
            bullet.Draw(spriteBatch);

        foreach (var creature in creatures)
            creature.Draw(spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var creature in creatures)
            creature.Update(gameTime);

        foreach (var bullet in BulletsManager.Bullets)
            bullet.Update(gameTime);

        BulletsManager.Update(creatures);
    }
}