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
    public abstract class PhysicalObject
    {
        public PhysicalObject()
        {
            animation = new AnimationData();
            exists = true;

            spritePosition = new Rectangle(16, 20, ApplicationGlobals.TILE_SIZE, ApplicationGlobals.TILE_SIZE);
            spriteSpeed = new Vector2(0.0f, 0.0f);
            spriteAcceleration = new Vector2(0.0f, 0.0f);
        } // ctor

        public virtual void load(ContentManager Content) { }

        public virtual void Update(GameTime gameTime, MapTile[,] map) { }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            if(exists)
                spriteBatch.Draw(pTexture, spritePosition, animation.imageSource, Color.White);
        } // draw

        protected bool exists;                  // Whether the object exists and should be displayed/updated

        protected Texture2D pTexture;           // Image to draw
        protected Rectangle spritePosition;     // Position
        protected Vector2 spriteSpeed;          // Velocity
        protected Vector2 spriteAcceleration;   // Acceleration

        protected AnimationData animation;      // Animation data
    }
}
