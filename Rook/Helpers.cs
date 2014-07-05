using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Rook
{
    public struct MapTile
    {
        public CollisionType collisionType;
        public Rectangle mapSrc;
        public Rectangle mapDest;
        public char displayChar;
    };

    public enum CollisionType
    {
        None,
        Full,
        Top,
        Bottom,
        Damage,
    };

    // TODO: THIS IS WHERE I LEFT OFF...
    public enum Owner
    {
        Player,
        Neutral,
        Enemy
    };
}
