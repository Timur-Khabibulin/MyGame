using Microsoft.Xna.Framework;

namespace MyGame.Code.Model.Entities;

public interface ICollidable
{
    public Rectangle ViewArea { get; }

    public bool IsCollided(Rectangle anotherViewArea);
}