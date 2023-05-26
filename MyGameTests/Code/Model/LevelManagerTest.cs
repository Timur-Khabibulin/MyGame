using MyGame.Code.Model;
using MyGame.Code.Model.Entities;

namespace MyGameTests.Code.Model;

public class LevelManagerTest
{
    [Test]
    public void DefaultLevelIsEasiest()
    {
        Assert.That(new LevelManager().Level, Is.EqualTo(Level.Easy));
    }

    [Test]
    public void LevelChange()
    {
        var manager = new LevelManager();
        var oldLevel = manager.Level;

        manager.ChangeLevel();
        var newLevel = manager.Level;

        Assert.That(newLevel, Is.Not.EqualTo(oldLevel));
    }

    [Test]
    public void OnLevelChangedIsCalled_WhenLevelChanged()
    {
        var manager = new LevelManager();
        var isCalled = false;
        var oldLevel = manager.Level;
        var newLevel = manager.Level;

        manager.OnLevelChanged += (level) =>
        {
            isCalled = true;
            newLevel = level;
        };

        manager.ChangeLevel();

        Assert.IsTrue(isCalled);
        Assert.That(newLevel, Is.Not.EqualTo(oldLevel));
    }
}