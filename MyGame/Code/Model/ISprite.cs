using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Code.Model;

public interface ISprite
{
    public Texture2D Texture { get; }
    public Rectangle ViewArea { get; }
    public Vector2 Position { get;}

    public Vector2 Min { get; }

    public Vector2 Max { get; }

    public void Draw(SpriteBatch spriteBatch);

    public void Update(GameTime gameTime);
}