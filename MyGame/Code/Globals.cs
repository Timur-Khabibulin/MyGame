﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame.Code;

public class Globals
{
    public Point Resolution { get; }
    public ContentManager ContentManager { get; }

    public Point PlayerTextureSize;
    public Point HunterTextureSize;
    public Point BulletTextureSize;

    public Globals(ContentManager contentManager,Point resolution)
    {
        Resolution = resolution;
        ContentManager = contentManager;
    }
}