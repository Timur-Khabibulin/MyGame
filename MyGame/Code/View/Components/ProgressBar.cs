using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Code.View.Components;

public class ProgressBar : IViewComponent
{
    public int Width { get; }
    public int Height { get; }

    private readonly Texture2D texture;
    private readonly double maxValue;

    private Rectangle viewPart;
    private Vector2 position;
    private double value;

    public ProgressBar(ContentManager contentManager, double maxValue, Vector2 position, int width, int height)
    {
        this.position = position;
        Width = width;
        Height = height;
        this.maxValue = maxValue;
        texture = contentManager.Load<Texture2D>(ResourceNames.Health);
        viewPart = new Rectangle(0, 0, width, height);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        => spriteBatch.Draw(texture, position, viewPart, Color.White);

    public void Update(GameTime gameTime)
    {
        viewPart.Width = (int)(value / maxValue * Width);
    }

    public void UpdateValue(double currentValue, Vector2 currentPosition)
    {
        value = currentValue;
        position = currentPosition;
    }
}