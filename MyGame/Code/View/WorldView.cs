using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Code.Model;
using MyGame.Code.Model.Entities;
using MyGame.Code.View.Components;

namespace MyGame.Code.View;

public class WorldView : IViewComponent
{
    private Texture2D playerTexture;
    private Texture2D hunterTexture;
    private Texture2D bullletTexture;
    private Texture2D groundTexture;
    private Texture2D skyTexture;
    private Texture2D sunsetTexture;
    private SpriteFont headerFont;
    private SpriteFont textFont;
    private Text gameOverText;

    private Globals globals;
    private World world;
    private ProgressBar progressBar;

    public WorldView(Globals globals, World world)
    {
        this.globals = globals;
        this.world = world;

        LoadContent();
        progressBar = new ProgressBar(globals.ContentManager,
            100,
            world.PlayerPosition,
            hunterTexture.Width,
            15);
        gameOverText = new Text("Вы проиграли", headerFont, new Color(97, 240, 232));
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!world.GameOver)
        {
            DrawWorld(spriteBatch);
            DrawEnemies(gameTime, spriteBatch);
            DrawBullets(spriteBatch);
            DrawPlayerInfo(spriteBatch);
        }
        else
        {
            DrawWhenGameOver(spriteBatch);
        }
    }

    public void Update(GameTime gameTime)
    {
    }

    private void LoadContent()
    {
        var contentManager = globals.ContentManager;
        playerTexture = contentManager.Load<Texture2D>(ResourceNames.Goose);
        hunterTexture = contentManager.Load<Texture2D>(ResourceNames.Hunter);
        bullletTexture = contentManager.Load<Texture2D>(ResourceNames.Bullet);
        groundTexture = contentManager.Load<Texture2D>(ResourceNames.Ground);
        skyTexture = contentManager.Load<Texture2D>(ResourceNames.Sky);
        sunsetTexture = contentManager.Load<Texture2D>(ResourceNames.Sunset);
        headerFont = contentManager.Load<SpriteFont>(ResourceNames.HeaderFont);
        textFont = contentManager.Load<SpriteFont>(ResourceNames.ButtonFont);

        globals.HunterTextureSize = new Point(hunterTexture.Width, hunterTexture.Height);
        globals.PlayerTextureSize = new Point(playerTexture.Width, playerTexture.Height);
        globals.BulletTextureSize = new Point(bullletTexture.Width, bullletTexture.Height);
    }


    private void DrawWorld(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(skyTexture,
            new Rectangle(0, 0, globals.Resolution.X, globals.Resolution.Y),
            Color.White);

        spriteBatch.Draw(groundTexture, new Rectangle(0,
            3 * globals.Resolution.Y / 4,
            globals.Resolution.X,
            globals.Resolution.Y / 4), Color.White);
    }

    private void DrawEnemies(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (var creature in world.CreaturesManager.Creatures)
        {
            if (!creature.IsDead)
            {
                var texture = creature.Type == CreatureType.Goose ? playerTexture : hunterTexture;

                progressBar.UpdateValue(creature.Health, creature.Position);
                progressBar.Update(gameTime);

                spriteBatch.Draw(texture, creature.Position, Color.White);
                progressBar.Draw(gameTime, spriteBatch);
            }
        }
    }

    private void DrawBullets(SpriteBatch spriteBatch)
    {
        foreach (var bullet in BulletsManager.Bullets)
        {
            if (bullet.IsActive)
                spriteBatch.Draw(bullletTexture, bullet.Position, Color.White);
        }
    }

    private void DrawPlayerInfo(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(headerFont,
            world.PlayerScore.ToString(),
            new Vector2(1000, 900),
            Color.White);
    }

    private void DrawWhenGameOver(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sunsetTexture,
            new Rectangle(0, 0, globals.Resolution.X, globals.Resolution.Y),
            Color.White);

        var textPos = new Vector2((globals.Resolution.X - gameOverText.Size.X) / 2,
            (globals.Resolution.Y - gameOverText.Size.Y) / 2);
        spriteBatch.DrawString(gameOverText.Font,
            gameOverText.Value,
            textPos,
            gameOverText.TextColor);

        spriteBatch.DrawString(textFont,
            $"Ваш счет : {world.PlayerScore}",
            new Vector2(textPos.X, textPos.Y + 200),
            gameOverText.TextColor);
    }
}