using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyGame.Code.Model.Entities;

namespace MyGame.Code.Model;

public class CreaturesManager
{
    public IReadOnlyCollection<ICreature> Creatures => creatures;

    private const double AppearancePeriod = 2;
    private readonly Vector2 hunterStartPosition = new(1600, 720);
    private readonly List<ICreature> creatures;
    private readonly ICreature player;
    private readonly Globals globals;

    private double timer;

    public CreaturesManager(Globals globals, ICreature player)
    {
        this.globals = globals;
        creatures = new List<ICreature>() { player };
        this.player = player;
    }

    public void Update(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.TotalSeconds - timer > AppearancePeriod)
        {
            creatures.Add(new Hunter(player, globals, hunterStartPosition));
            timer = gameTime.TotalGameTime.TotalSeconds;
        }

        foreach (var creature in creatures)
            creature.Act(gameTime);
    }
}