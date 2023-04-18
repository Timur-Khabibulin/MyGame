using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public interface IComponent
{
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    public void Update(GameTime gameTime);
    
}