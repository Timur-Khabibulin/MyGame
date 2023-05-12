using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = System.Numerics.Vector2;

namespace MyGame.Code.View;

public class Button : IComponent
{
    public event Action OnClick;
    public Text Text { get; init; }
    public Point Size { get; }
    public Point Position { get; }

    private Texture2D texture;
    private Rectangle rectangle;

    public Button(Point position, Point size, Texture2D texture)
    {
        this.Position = position;
        this.Size = size;
        this.texture = texture;
        rectangle = new Rectangle(position, size);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, rectangle, Color.White);

        if (Text != null)
        {
            var x = (rectangle.X + rectangle.Width / 2) - (Text.Font.MeasureString(Text.Value).X / 2);
            var y = (rectangle.Y + rectangle.Height / 2) - (Text.Font.MeasureString(Text.Value).Y / 2);

            spriteBatch.DrawString(Text.Font, Text.Value, new Vector2(x, y), Text.TextColor);
        }
    }

    public void Update(GameTime gameTime)
    {
        var mouse = Mouse.GetState();

        if (rectangle.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Pressed)
            OnClick?.Invoke();
    }
}