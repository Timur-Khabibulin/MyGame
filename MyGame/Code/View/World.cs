using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame;

public class World : IComponent
{
    private Texture2D ground;
    private int width;
    private int height;
    private int groundHeight;

    public World(ContentManager content, int width, int height)
    {
        ground = content.Load<Texture2D>("Ground");
        this.width = width;
        this.height = height;
        groundHeight = height / 4;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(ground, new Rectangle(0,
            height - groundHeight,
            width,
            groundHeight), Color.White);
    }

    public void Update(GameTime gameTime)
    {
    }
}