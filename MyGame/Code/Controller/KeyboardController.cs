using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame.Code.Controller;

public static class KeyboardController
{
    public static event Action OnLeft;
    public static event Action OnRight;
    public static event Action OnUp;
    public static event Action OnDown;

    public static event Action OnBack;

    public static event Action<GameTime, Vector2> OnLeftMouseClick;

    public static void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();

        if (keyboardState.IsKeyDown(InputConfig.Up)) OnUp?.Invoke();

        if (keyboardState.IsKeyDown(InputConfig.Down)) OnDown?.Invoke();

        if (keyboardState.IsKeyDown(InputConfig.Left)) OnLeft?.Invoke();

        if (keyboardState.IsKeyDown(InputConfig.Right)) OnRight?.Invoke();

        if (keyboardState.IsKeyDown(InputConfig.Back)) OnBack?.Invoke();

        if (mouseState.LeftButton == ButtonState.Pressed)
            OnLeftMouseClick?.Invoke(gameTime, mouseState.Position.ToVector2());
    }
}