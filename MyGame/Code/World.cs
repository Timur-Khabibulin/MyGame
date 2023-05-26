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
    public int PlayerScore => player.Score;
    public bool GameOver => player.IsDead;
    public Level GameLevel { get; private set; }

    private readonly Globals globals;
    private Goose player;

    public World(Globals globals, Level level)
    {
        this.globals = globals;
        GameLevel = level;
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

    public void OnLevelChanged(Level currentLevel)
    {
        GameLevel = currentLevel;
        CreaturesManager.LevelChanged(GameLevel);
    }

    private void RecreateWorld()
    {
        BulletsManager.RemoveAll();

        player = new Goose(globals, new Vector2(50, 10));

        KeyboardController.OnDown += player.MoveDown;
        KeyboardController.OnLeft += player.MoveLeft;
        KeyboardController.OnRight += player.MoveRight;
        KeyboardController.OnUp += player.MoveUp;
        KeyboardController.OnLeftMousePress += player.Attack;
        KeyboardController.OnBack += Exit;

        CreaturesManager = new CreaturesManager(globals, player, GameLevel);
    }

    private void Exit()
    {
        if (GameOver) RecreateWorld();
        OnStopGame?.Invoke();
    }
}