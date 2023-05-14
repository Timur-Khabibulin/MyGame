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
    private SpriteFont headerFont;
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
        gameOverText = new Text("Вы проиграли", headerFont, Color.Red);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!world.GameOver)
        {
            DrawWorld(spriteBatch);
            DrawEnemies(gameTime, spriteBatch);
            DrawBullets(spriteBatch);
        }
        else
        {
            spriteBatch.DrawString(gameOverText.Font,
                gameOverText.Value,
                new Vector2(500, 500),
                gameOverText.TextColor);
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
        headerFont = contentManager.Load<SpriteFont>(ResourceNames.HeaderFont);

        globals.HunterTextureSize = new Point(hunterTexture.Width, hunterTexture.Height);
        globals.PlayerTextureSize = new Point(playerTexture.Width, playerTexture.Height);
        globals.BulletTextureSize = new Point(bullletTexture.Width, bullletTexture.Height);
    }


    private void DrawWorld(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(groundTexture, new Rectangle(0,
            3 * globals.Resolution.Y / 4,
            globals.Resolution.X,
            globals.Resolution.Y / 4), Color.White);
    }

    private void DrawBullets(SpriteBatch spriteBatch)
    {
        foreach (var bullet in BulletsManager.Bullets)
        {
            if (bullet.IsActive)
                spriteBatch.Draw(bullletTexture, bullet.Position, Color.White);
        }
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
}