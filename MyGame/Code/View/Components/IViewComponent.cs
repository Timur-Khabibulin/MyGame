using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Code.View.Components;

internal interface IViewComponent
{
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    public void Update(GameTime gameTime);
}