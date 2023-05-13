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
    public bool GameOver => player.IsDead;

    private readonly Texture2D ground;
    private readonly int width;
    private readonly int height;
    private readonly int groundHeight;
    private readonly SpriteFont font;
    private readonly ContentManager contentManager;

    private Goose player;
    private CreaturesManager creaturesManager;

    public World(ContentManager content, int width, int height)
    {
        contentManager = content;

        font = contentManager.Load<SpriteFont>(ResourceNames.HeaderFont);

        ground = contentManager.Load<Texture2D>(ResourceNames.Ground);
        this.width = width;
        this.height = height;
        groundHeight = height / 4;

        RecreateWorld();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (GameOver)
        {
            var text = new Text("Вы проиграли", font, Color.Red);
            spriteBatch.DrawString(text.Font, text.Value, new Vector2(500, 500), text.TextColor);
        }
        else
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
    }

    public void Update(GameTime gameTime)
    {
        if (!GameOver)
        {
            BulletsManager.Update(creaturesManager.Creatures);
            creaturesManager.Update(gameTime);

            foreach (var creature in creaturesManager.Creatures)
                creature.Update(gameTime);

            foreach (var bullet in BulletsManager.Bullets)
                bullet.Update(gameTime);
        }
    }

    private void Exit()
    {
        if (GameOver) RecreateWorld();
        OnStopGame?.Invoke();
    }

    private void RecreateWorld()
    {
        BulletsManager.RemoveAll();

        player = new Goose(new Vector2(50, 10),
            contentManager,
            new Vector2(50, 10),
            new Vector2(width * 0.6f, height * 0.6f));

        Controller.OndOwn += player.MoveDown;
        Controller.OnLeft += player.MoveLeft;
        Controller.OnRight += player.MoveRight;
        Controller.OnUp += player.MoveUp;
        Controller.OnLeftMouseClick += player.Attack;
        Controller.OnBack += Exit;

        creaturesManager = new CreaturesManager(contentManager, player, width, height);
    }
}