using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Code;
using MyGame.Code.Model;
using MyGame.Code.View;

namespace MyGame;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private SplashScreen splashScreen;
    private World world;
    private GameState gameState;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = 1920;
        graphics.PreferredBackBufferHeight = 1080;
        //graphics.ToggleFullScreen();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        splashScreen = new SplashScreen(Content);

        splashScreen.OnStartGame += () => gameState = GameState.Game;
        splashScreen.OnExit += Exit;

        world = new World(Content,
            graphics.PreferredBackBufferWidth,
            graphics.PreferredBackBufferHeight);

        world.OnStopGame += () => gameState = GameState.SplashScreen;
    }

    protected override void Update(GameTime gameTime)
    {
        Controller.Update(gameTime);
        switch (gameState)
        {
            case GameState.SplashScreen:
                splashScreen.Update(gameTime);
                break;
            case GameState.Game:
                world.Update(gameTime);
                break;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin();

        switch (gameState)
        {
            case GameState.SplashScreen:
                splashScreen.Draw(gameTime, spriteBatch);
                break;
            case GameState.Game:
                world.Draw(gameTime, spriteBatch);
                break;
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}