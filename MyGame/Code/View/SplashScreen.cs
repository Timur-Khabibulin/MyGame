using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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

    private readonly List<IViewComponent> components = new();
    private readonly Button playButton;
    private readonly Button exitButton;

    public SplashScreen(ContentManager content)
    {
        var font = content.Load<SpriteFont>(ResourceNames.ButtonFont);

        playButton = new Button(new Point(400, 400),
            new Point(150, 50),
            content.Load<Texture2D>(ResourceNames.Button))
        {
            Text = new Text("Играть", font, Color.Gray)
        };
        components.Add(playButton);

        exitButton = new Button(new Point(playButton.Size.X + 50, 400),
            new Point(150, 50),
            content.Load<Texture2D>(ResourceNames.Button))
        {
            Text = new Text("Выход", font, Color.Gray)
        };
        components.Add(exitButton);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (var component in components)
            component.Draw(gameTime, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var component in components)
            component.Update(gameTime);
    }
}