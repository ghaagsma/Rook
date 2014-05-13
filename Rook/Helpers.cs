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

    public enum AnimationType
    {
        Default = 0,
        MoveLeft1 = 1,
        FaceLeft = 2,
        MoveLeft2 = 3,
        MoveRight1 = 4,
        FaceRight = 5,
        MoveRight2 = 6,
        TakingDamage = 7,
        GivingDamage = 8,
        Dying = 9,
        Dead = 10
    };
}
