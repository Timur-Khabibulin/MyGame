using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Code.Controller;
using MyGame.Code.Model;
using MyGame.Code.View;

namespace MyGame.Code;

public class GameRoot : Game
{
    private SpriteBatch spriteBatch;
    private SplashScreen splashScreen;
    private World world;
    private GameState gameState;
    private Point resolution;
    private Globals globals;
    private WorldView worldView;

    public GameRoot()
    {
        var graphics = new GraphicsDeviceManager(this);
        resolution = new Point(1920, 1080);
        graphics.PreferredBackBufferWidth = resolution.X;
        graphics.PreferredBackBufferHeight = resolution.Y;
        graphics.ToggleFullScreen();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        var levelManager = new LevelManager();

        globals = new Globals(Content, resolution);
        splashScreen = new SplashScreen(globals, levelManager);

        splashScreen.OnStartGame += () => gameState = GameState.Game;
        splashScreen.OnExit += Exit;

        world = new World(globals, levelManager.Level);
        worldView = new WorldView(globals, world);
        levelManager.LevelChanged += world.LevelChanged;

        world.OnStopGame += () => gameState = GameState.SplashScreen;
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardController.Update(gameTime);
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
                worldView.Draw(gameTime, spriteBatch);
                break;
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}