using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MyGame.Code.Model.Entities;

namespace MyGame.Code.Model;

public class CreaturesManager
{
    public IReadOnlyCollection<BaseCreature> Creatures => creatures;

    private const double AppearancePeriod = 2;
    private readonly Vector2 hunterStartPosition = new(1600, 720);

    private readonly ContentManager contentManager;
    private readonly List<BaseCreature> creatures;
    private readonly Goose player;
    private double timer;
    private int width;
    private int height;

    public CreaturesManager(ContentManager contentManager, BaseCreature player, int width, int height)
    {
        this.contentManager = contentManager;
        creatures = new List<BaseCreature>() { player };
        this.player = (Goose)player;
        this.width = width;
        this.height = height;
    }

    public void Update(GameTime gameTime)
    {
        if (gameTime.TotalGameTime.TotalSeconds - timer > AppearancePeriod)
        {
            creatures.Add(new Hunter(player, hunterStartPosition,
                contentManager,
                new Vector2(200, 0),
                new Vector2(width - 50, height)));
            timer = gameTime.TotalGameTime.TotalSeconds;
        }
    }
}