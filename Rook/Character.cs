using Microsoft.Xna.Framework;
using System;

namespace Rook
{
    public class Character : PhysicalObject
    {
        public Character()
        {
            IsAirborne = false;

            MaxHealth = 200;
            MaxMana = 100;
            Health = 200;
            Mana = 100;
        }

        protected void Move(MapTile[,] map, GameTime gameTime)
        {
            int xTile1, xTile2, yTile1, yTile2;

            // Move Horizontally
            for (var i = 0; i < (int)Math.Abs(SpriteSpeed.X); i++)
            {
                SpritePosition.X += (int)(SpriteSpeed.X / Math.Abs(SpriteSpeed.X));

                xTile1 = (SpritePosition.X + 3) / ApplicationGlobals.TILE_SIZE;
                xTile2 = (SpritePosition.X + ApplicationGlobals.TILE_SIZE - 4) / ApplicationGlobals.TILE_SIZE;
                yTile1 = SpritePosition.Y / ApplicationGlobals.TILE_SIZE;
                yTile2 = (SpritePosition.Y + ApplicationGlobals.TILE_SIZE - 1) / ApplicationGlobals.TILE_SIZE;

                if (SpritePosition.X < 0 || 
                    SpritePosition.X > ApplicationGlobals.MAP_WIDTH - ApplicationGlobals.TILE_SIZE ||
                    map[yTile1, xTile1].CollisionType == CollisionType.Full ||
                    map[yTile2, xTile1].CollisionType == CollisionType.Full ||
                    map[yTile1, xTile2].CollisionType == CollisionType.Full ||
                    map[yTile2, xTile2].CollisionType == CollisionType.Full)
                {
                    SpritePosition.X -= (int)(SpriteSpeed.X / Math.Abs(SpriteSpeed.X));
                    SpriteSpeed.X = 0;
                }
            }

            // Move Vertically
            for (var i = 0; i < (int)Math.Abs(SpriteSpeed.Y); i++)
            {
                SpritePosition.Y += (int)(SpriteSpeed.Y / Math.Abs(SpriteSpeed.Y));

                xTile1 = (SpritePosition.X + 3) / ApplicationGlobals.TILE_SIZE;
                xTile2 = (SpritePosition.X + ApplicationGlobals.TILE_SIZE - 4) / ApplicationGlobals.TILE_SIZE;
                yTile1 = SpritePosition.Y / ApplicationGlobals.TILE_SIZE;
                yTile2 = (SpritePosition.Y + ApplicationGlobals.TILE_SIZE - 1) / ApplicationGlobals.TILE_SIZE;

                if (SpritePosition.Y < 0 ||
                    SpritePosition.Y > ApplicationGlobals.MAP_HEIGHT - ApplicationGlobals.TILE_SIZE ||
                    map[yTile1, xTile1].CollisionType == CollisionType.Full ||
                    map[yTile2, xTile1].CollisionType == CollisionType.Full ||
                    map[yTile1, xTile2].CollisionType == CollisionType.Full ||
                    map[yTile2, xTile2].CollisionType == CollisionType.Full)
                {
                    // If character was moving upward, they are still airborne.
                    // Otherwise, they just landed.
                    IsAirborne = SpriteSpeed.Y <= 0;

                    SpritePosition.Y -= (int)(SpriteSpeed.Y / Math.Abs(SpriteSpeed.Y));
                    SpriteSpeed.Y = 0;
                }
            }

            // Allow character to jump for a slight instant after falling off an edge
            if (SpriteSpeed.Y > 2)
                IsAirborne = true;

            // Check for map collision damage
            xTile1 = (SpritePosition.X + 1) / ApplicationGlobals.TILE_SIZE;
            xTile2 = (SpritePosition.X + ApplicationGlobals.TILE_SIZE - 2) / ApplicationGlobals.TILE_SIZE;
            yTile1 = SpritePosition.Y / ApplicationGlobals.TILE_SIZE;
            yTile2 = (SpritePosition.Y + ApplicationGlobals.TILE_SIZE - 1) / ApplicationGlobals.TILE_SIZE;

            if (map[yTile1, xTile1].CollisionType == CollisionType.Damage ||
                map[yTile2, xTile1].CollisionType == CollisionType.Damage ||
                map[yTile1, xTile2].CollisionType == CollisionType.Damage ||
                map[yTile2, xTile2].CollisionType == CollisionType.Damage)
            {
                TakeDamage(1);
            }
        }

        virtual public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
                Kill();
            // TODO: Make invulnerable for short time after taking damage
            //else
            //{

            //}
        }

        virtual public void Kill()
        { 
            
        }

        protected bool IsAirborne;

        protected float TerminalVelocity = 11.0f;
        protected float Gravity = 0.4f;

        protected int MaxHealth;
        protected int MaxMana;
        protected int Health;
        protected int Mana;

        protected int MaxMeleeDamage;
        protected int MaxMagicDamage;
        protected int MagicRange;
    }
}
