using System;
using Microsoft.Xna.Framework;
using MyGame.Code.Controller;
using MyGame.Code.Model;
using MyGame.Code.Model.Entities;

namespace MyGame.Code;

public class World
{
    public CreaturesManager CreaturesManager { get; private set; }
    public event Action OnStopGame;
    public Vector2 PlayerPosition => player.Position;
    public bool GameOver => player.IsDead;

    private readonly Globals globals;
    private Goose player;

    public World(Globals globals)
    {
        this.globals = globals;
        RecreateWorld();
    }

    public void Update(GameTime gameTime)
    {
        if (!GameOver)
        {
            BulletsManager.Update(CreaturesManager.Creatures);
            CreaturesManager.Update(gameTime);
        }
    }

    private void RecreateWorld()
    {
        BulletsManager.RemoveAll();

        player = new Goose(globals, new Vector2(50, 10));

        KeyboardController.OnDown += player.MoveDown;
        KeyboardController.OnLeft += player.MoveLeft;
        KeyboardController.OnRight += player.MoveRight;
        KeyboardController.OnUp += player.MoveUp;
        KeyboardController.OnLeftMouseClick += player.Attack;
        KeyboardController.OnBack += Exit;

        CreaturesManager = new CreaturesManager(globals, player);
    }

    private void Exit()
    {
        if (GameOver) RecreateWorld();
        OnStopGame?.Invoke();
    }
}