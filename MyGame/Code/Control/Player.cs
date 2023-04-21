using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Model;

namespace MyGame.Code.Control;

public class Player : IControl
{
    private ICreature entity { get; }
    private Texture2D texture;
    private int windowWidth;
    private int windowHeight;
    private Vector2 startPosition = new Vector2(50, 10);

    public Player(ContentManager content, int windowWidth, int windowHeight)
    {
        this.windowHeight = windowHeight;
        this.windowWidth = windowWidth;
        entity = new Goose(startPosition);
        texture = content.Load<Texture2D>(entity.AssetName);
    }

    public void Act(KeyboardState keyboardState)
    {
        if (keyboardState.IsKeyDown(InputConfig.Up))
            TryMove(-entity.VerticalShift);

        if (keyboardState.IsKeyDown(InputConfig.Down))
            TryMove(entity.VerticalShift);

        if (keyboardState.IsKeyDown(InputConfig.Left))
            TryMove(-entity.HorizontalShift);

        if (keyboardState.IsKeyDown(InputConfig.Right))
            TryMove(entity.HorizontalShift);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, entity.Position, Color.White);
    }

    private void TryMove(Vector2 shift)
    {
        var newPosition = entity.Position + shift;
        if (newPosition.X >= startPosition.X && newPosition.X < windowWidth - 250 &&
            newPosition.Y >= startPosition.Y && newPosition.Y < windowHeight - 400)
            entity.Position = newPosition;
    }
}