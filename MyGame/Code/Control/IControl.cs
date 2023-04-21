using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Model;

namespace MyGame.Code.Control;

public interface IControl
{
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    public void Act(KeyboardState keyboardState);
}