using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Code.Model;
using MyGame.Code.Model.Entities;

namespace MyGame.Code.View;

public class World : IComponent
{
    public event Action OnStopGame;

    private readonly Texture2D ground;
    private readonly int width;
    private readonly int height;
    private readonly int groundHeight;

    private readonly CreaturesManager creaturesManager;

    public World(ContentManager content, int width, int height)
    {
        ground = content.Load<Texture2D>(ResourceNames.Ground);
        this.width = width;
        this.height = height;
        groundHeight = height / 4;

        var player = new Goose(new Vector2(50, 10),
            content,
            new Vector2(50, 10),
            new Vector2(width * 0.6f, height * 0.6f));

        creaturesManager = new CreaturesManager(content, player, this.width, this.height);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(ground, new Rectangle(0,
            height - groundHeight,
            width,
            groundHeight), Color.White);

        foreach (var bullet in BulletsManager.Bullets)
            bullet.Draw(spriteBatch);

        foreach (var creature in creaturesManager.Creatures)
            creature.Draw(spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        BulletsManager.Update(creaturesManager.Creatures);
        creaturesManager.Update(gameTime);

        foreach (var creature in creaturesManager.Creatures)
            creature.Update(gameTime);

        foreach (var bullet in BulletsManager.Bullets)
            bullet.Update(gameTime);

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape)) OnStopGame?.Invoke();
    }
}