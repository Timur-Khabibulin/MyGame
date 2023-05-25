using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Code.View.Components;

namespace MyGame.Code.View

;

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
    private Color TextColor=>new(66, 66, 66);
    private Point ButtonSize => new(450, 150);
    private Point FirstButtonPosition => new((globals.Resolution.X - ButtonSize.X) / 2, 350);

    private readonly Globals globals;
    private readonly List<IViewComponent> components = new();
    private readonly Button playButton;
    private readonly Button exitButton;
    private readonly Texture2D background;


    public SplashScreen(Globals globals)
    {
        this.globals = globals;
        var font = globals.ContentManager.Load<SpriteFont>(ResourceNames.ButtonFont);
        background = globals.ContentManager.Load<Texture2D>(ResourceNames.SplashScreen);

        playButton = new Button(new Point(FirstButtonPosition.X, FirstButtonPosition.Y),
            ButtonSize,
            globals.ContentManager.Load<Texture2D>(ResourceNames.Button))
        {
            Text = new Text("Играть", font, TextColor),
            BackgroundColor = BtnColor
        };

        components.Add(playButton);

        exitButton = new Button(new Point(FirstButtonPosition.X, FirstButtonPosition.Y + playButton.Size.Y + 50),
            ButtonSize,
            globals.ContentManager.Load<Texture2D>(ResourceNames.Button))
        {
            Text = new Text("Выход", font, TextColor),
            BackgroundColor = BtnColor
        };
        components.Add(exitButton);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background,
            new Rectangle(0, 0, globals.Resolution.X, globals.Resolution.Y),
            Color.White);

        foreach (var component in components)
            component.Draw(gameTime, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var component in components)
            component.Update(gameTime);
    }
}