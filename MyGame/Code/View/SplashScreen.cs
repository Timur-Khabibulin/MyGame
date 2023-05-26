using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Code.Model;
using MyGame.Code.Model.Entities;
using MyGame.Code.View.Components;

namespace MyGame.Code.View;

public class SplashScreen : IViewComponent
{
    public event Action OnExit
    {
        add => exitButton.OnClick += value;
        remove => exitButton.OnClick -= value;
    }

    public event Action OnStartGame
    {
        add => playButton.OnClick += value;
        remove => playButton.OnClick -= value;
    }

    private Color BtnColor => new(247, 209, 166);
    private Color TextColor => new(66, 66, 66);
    private Point ButtonSize => new(450, 150);
    private Point FirstButtonPosition => new((globals.Resolution.X - ButtonSize.X) / 2, 350);

    private readonly Globals globals;
    private readonly List<IViewComponent> components = new();
    private readonly Button playButton;
    private readonly Button exitButton;
    private readonly Button levelSelector;
    private readonly Texture2D background;
    private readonly SpriteFont btnFont;
    private readonly LevelManager levelManager;

    private readonly Dictionary<Level, string> levelsNames = new()
    {
        { Level.Easy, "Легкий" },
        { Level.Middle, "Средний" },
        { Level.Hard, "Сложный" }
    };

    public SplashScreen(Globals globals, LevelManager levelManager)
    {
        this.globals = globals;
        this.levelManager = levelManager;
        btnFont = globals.ContentManager.Load<SpriteFont>(ResourceNames.ButtonFont);
        background = globals.ContentManager.Load<Texture2D>(ResourceNames.SplashScreen);

        playButton = new Button(new Point(FirstButtonPosition.X, FirstButtonPosition.Y),
            ButtonSize,
            globals.ContentManager.Load<Texture2D>(ResourceNames.Button))
        {
            Text = new Text("Играть", btnFont, TextColor),
            BackgroundColor = BtnColor
        };

        components.Add(playButton);

        exitButton = new Button(new Point(FirstButtonPosition.X, FirstButtonPosition.Y + playButton.Size.Y + 50),
            ButtonSize,
            globals.ContentManager.Load<Texture2D>(ResourceNames.Button))
        {
            Text = new Text("Выход", btnFont, TextColor),
            BackgroundColor = BtnColor
        };
        components.Add(exitButton);

        levelSelector = new Button(new Point(300, 800),
            new Point(550, 110),
            globals.ContentManager.Load<Texture2D>(ResourceNames.Button))
        {
            Text = new Text("Сменить уровень", btnFont, TextColor),
            BackgroundColor = BtnColor
        };
        levelSelector.OnClick += this.levelManager.ChangeLevel;
        
        components.Add(levelSelector);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background,
            new Rectangle(0, 0, globals.Resolution.X, globals.Resolution.Y),
            Color.White);

        foreach (var component in components)
            component.Draw(gameTime, spriteBatch);


        spriteBatch.DrawString(btnFont,
            $"Текущий уровень : {levelsNames[levelManager.Level]}",
            new Vector2(300 + levelSelector.Size.X + 70, 830),
            Color.White);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var component in components)
            component.Update(gameTime);
    }
}