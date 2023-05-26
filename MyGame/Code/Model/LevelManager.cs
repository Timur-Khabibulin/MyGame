using System;
using MyGame.Code.Model.Entities;

namespace MyGame.Code.Model;

public class LevelManager
{
    public Level Level { get; private set; }
    public event Action<Level> LevelChanged;

    public LevelManager()
    {
        Level = Level.Easy;
    }

    public void ChangeLevel()
    {
        var count = Enum.GetValues<Level>().Length;
        Level = (Level)((int)(Level + 1) % count);
        LevelChanged?.Invoke(Level);
    }
}