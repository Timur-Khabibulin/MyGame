using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Code.Control;
using MyGame.Model;

namespace MyGame;

public class World : IComponent
{
    private Texture2D ground;
    private int width;
    private int height;
    private int groundHeight;

    private Player player;

    public World(ContentManager content, int width, int height)
    {
        ground = content.Load<Texture2D>(ResourceNames.Ground);
        this.width = width;
        this.height = height;
        groundHeight = height / 4;
        player = new Player(content, width, height);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(ground, new Rectangle(0,
            height - groundHeight,
            width,
            groundHeight), Color.White);

        player.Draw(gameTime, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();
        player.Act(keyboardState);
    }
}