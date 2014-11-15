using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rook
{
    public abstract class PhysicalObject
    {
        protected PhysicalObject()
        {
            //Animation = new Animation();
            Exists = true;

            SpritePosition = new Rectangle(16, 20, ApplicationGlobals.TILE_SIZE, ApplicationGlobals.TILE_SIZE);
            SpriteSpeed = new Vector2(0.0f, 0.0f);
            SpriteAcceleration = new Vector2(0.0f, 0.0f);
        } // ctor

        public virtual void Load(ContentManager content) { }

        public virtual void Update(GameTime gameTime, MapTile[,] map) { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(Exists)
                spriteBatch.Draw(PTexture, SpritePosition, Animation.ImageSource, Color.White);
        } // draw

        protected bool Exists;                  // Whether the object exists and should be displayed/updated

        protected Texture2D PTexture;           // Image to draw
        protected Rectangle SpritePosition;     // Position
        protected Vector2 SpriteSpeed;          // Velocity
        protected Vector2 SpriteAcceleration;   // Acceleration

        protected Animation Animation;          // Animation data
    }
}
