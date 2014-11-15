using Microsoft.Xna.Framework;

namespace Rook
{
    public struct MapTile
    {
        public CollisionType CollisionType;
        public Rectangle MapSrc;
        public Rectangle MapDest;
        public char DisplayChar;
    };

    public enum CollisionType
    {
        None,
        Full,
        Top,
        Bottom,
        Damage,
    };

    // TODO: Implement this
    public enum Owner
    {
        Player,
        Neutral,
        Enemy
    };
}
