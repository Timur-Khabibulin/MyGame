using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyGame.Code.Model.Entities;

namespace MyGame.Code.Model;

public class CreaturesManager
{
    public IReadOnlyCollection<ICreature> Creatures => creatures;

    private readonly double appearancePeriod = 2;
    private readonly Vector2 hunterStartPosition = new(1600, 720);
    private readonly List<ICreature> creatures;
    private readonly ICreature player;
    private readonly Globals globals;
    private Level level;

    private double timer;

    public CreaturesManager(Globals globals, ICreature player, Level level)
    {
        this.globals = globals;
        creatures = new List<ICreature>() { player };
        this.player = player;
        this.level = level;
        appearancePeriod /= (int)(level + 1);
    }

    public void Update(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.TotalSeconds - timer > appearancePeriod)
        {
            creatures.Add(GetHunterByLevel());
            timer = gameTime.TotalGameTime.TotalSeconds;
        }

        foreach (var creature in creatures)
            if (!creature.IsDead)
                creature.Act(gameTime);
    }

    public void LevelChanged(Level currentLevel) => level = currentLevel;

    private ICreature GetHunterByLevel() => level switch
    {
        Level.Easy => new Hunter(player, globals, hunterStartPosition, 3, 1, 3, 100),
        Level.Middle => new Hunter(player, globals, hunterStartPosition, 1, 0.5, 3, 100),
        Level.Hard => new Hunter(player, globals, hunterStartPosition, 0.5, 0.1, 3, 100),
    };
}