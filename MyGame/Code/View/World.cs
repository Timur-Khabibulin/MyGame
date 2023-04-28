using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Code.Control;
using MyGame.Model;

namespace MyGame;

public class World : IComponent
{
    private Texture2D ground;
    private int width;
    private int height;
    private int groundHeight;

    private List<IControl> entities;
    private List<Bullet> bullets;

    public World(ContentManager content, int width, int height)
    {
        ground = content.Load<Texture2D>(ResourceNames.Ground);
        this.width = width;
        this.height = height;
        groundHeight = height / 4;
        entities = new List<IControl>();
        bullets = new List<Bullet>();

        entities.Add(new Player(content, bullets, width, height));
        entities.Add(new Hunter(content, bullets, entities[0] as Player));
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(ground, new Rectangle(0,
            height - groundHeight,
            width,
            groundHeight), Color.White);

        foreach (var entity in entities)
            entity.Draw(gameTime, spriteBatch);

        foreach (var bullet in bullets)
            bullet.Draw(spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();

        foreach (var entity in entities)
            entity.Update(gameTime, keyboardState, mouseState);

        foreach (var bullet in bullets)
            bullet.Update();
    }
}