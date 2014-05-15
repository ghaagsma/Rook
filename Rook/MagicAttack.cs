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
    class MagicAttack : PhysicalObject
    {
        protected int damage;
        protected bool friendlyFire;

        public MagicAttack(int attackDamage, Rectangle position, Vector2 velocity, bool friendly)
        {
            damage = attackDamage;
            spritePosition = position;
            spriteSpeed = velocity;
            friendlyFire = friendly;
        }

        public override void Load(ContentManager Content){}
    }
}
