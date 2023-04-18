using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = System.Numerics.Vector2;

namespace MyGame;

public class SplashScreen : IComponent
{
    public Button PlayButton { get; }

    public SplashScreen(ContentManager content)
    {
        var font = content.Load<SpriteFont>("ButtonFont");

        PlayButton = new Button(new Point(10, 10),
            new Point(150, 50),
            content.Load<Texture2D>("Button"))
        {
            Text = new Text("Играть", font, Color.Gray)
        };
        PlayButton.OnClick += OpenGame;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        PlayButton.Draw(gameTime, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        PlayButton.Update(gameTime);
    }

    private void OpenGame(object sender, System.EventArgs e)
    {
        
    }
}