using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Code.View;

public class ProgressBar
{
    public int Width { get; }
    public int Height { get; }

    private Rectangle viewPart;
    private Vector2 position;
    private readonly Texture2D texture;
    private readonly double maxValue;

    public ProgressBar(ContentManager contentManager, double maxValue, Vector2 position, int width, int height)
    {
        this.position = position;
        Width = width;
        Height = height;
        this.maxValue = maxValue;
        texture = contentManager.Load<Texture2D>(ResourceNames.Health);
        viewPart = new Rectangle(0, 0, width, height);
    }

    public void Draw(SpriteBatch spriteBatch)
        => spriteBatch.Draw(texture, position, viewPart, Color.White);

    public void Update(double currentValue, Vector2 currentPostion)
    {
        viewPart.Width = (int)(currentValue / maxValue * Width);
        position = currentPostion;
    }
}