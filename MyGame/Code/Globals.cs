using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Code.Model;

namespace MyGame.Code;

public class Globals
{
    public Point Resolution { get; }
    public ContentManager ContentManager { get; }

    public Point PlayerTextureSize;
    public Point HunterTextureSize;
    public Point BulletTextureSize;

    public Globals(Point resolution, ContentManager contentManager = null)
    {
        Resolution = resolution;
        ContentManager = contentManager;
    }
}