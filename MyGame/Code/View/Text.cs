using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame;

public class Text
{
    public string Value { get; }
    public SpriteFont Font { get; }
    public Color TextColor { get; }

    public Text(string value, SpriteFont font, Color textColor)
    {
        Value = value;
        Font = font;
        TextColor = textColor;
    }
}