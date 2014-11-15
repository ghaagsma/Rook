using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Rook
{
    class Projectile : PhysicalObject
    {
        protected int Damage;
        protected int Owner;
        protected int Range;

        public Projectile(int damage, int owner, int range, Rectangle position, Vector2 velocity)
        {
            Damage = damage;
            Owner = owner;
            Range = range;
            SpritePosition = position;
            SpriteSpeed = velocity;
        }

        public override void Load(ContentManager content) {}
    }
}
