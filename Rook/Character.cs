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
    public class Character : PhysicalObject
    {
        public Character()
        {
            isAirborne = false;

            maxHealth = 200;
            maxMana = 100;
            health = 200;
            mana = 100;
        }

        protected void Move(MapTile[,] map, GameTime gameTime)
        {
            int xTile1, xTile2, yTile1, yTile2;

            // Move Horizontally
            for (int i = 0; i < (int)Math.Abs(spriteSpeed.X); i++)
            {
                spritePosition.X += (int)(spriteSpeed.X / Math.Abs(spriteSpeed.X));

                xTile1 = (spritePosition.X + 3) / ApplicationGlobals.TILE_SIZE;
                xTile2 = (spritePosition.X + ApplicationGlobals.TILE_SIZE - 4) / ApplicationGlobals.TILE_SIZE;
                yTile1 = spritePosition.Y / ApplicationGlobals.TILE_SIZE;
                yTile2 = (spritePosition.Y + ApplicationGlobals.TILE_SIZE - 1) / ApplicationGlobals.TILE_SIZE;

                if (spritePosition.X < 0 || 
                    spritePosition.X > ApplicationGlobals.MAP_WIDTH - ApplicationGlobals.TILE_SIZE ||
                    map[yTile1, xTile1].collisionType == CollisionType.Full ||
                    map[yTile2, xTile1].collisionType == CollisionType.Full ||
                    map[yTile1, xTile2].collisionType == CollisionType.Full ||
                    map[yTile2, xTile2].collisionType == CollisionType.Full)
                {
                    spritePosition.X -= (int)(spriteSpeed.X / Math.Abs(spriteSpeed.X));
                    spriteSpeed.X = 0;
                }
            }

            // Move Vertically
            for (int i = 0; i < (int)Math.Abs(spriteSpeed.Y); i++)
            {
                spritePosition.Y += (int)(spriteSpeed.Y / Math.Abs(spriteSpeed.Y));

                xTile1 = (spritePosition.X + 3) / ApplicationGlobals.TILE_SIZE;
                xTile2 = (spritePosition.X + ApplicationGlobals.TILE_SIZE - 4) / ApplicationGlobals.TILE_SIZE;
                yTile1 = spritePosition.Y / ApplicationGlobals.TILE_SIZE;
                yTile2 = (spritePosition.Y + ApplicationGlobals.TILE_SIZE - 1) / ApplicationGlobals.TILE_SIZE;

                if (spritePosition.Y < 0 ||
                    spritePosition.Y > ApplicationGlobals.MAP_HEIGHT - ApplicationGlobals.TILE_SIZE ||
                    map[yTile1, xTile1].collisionType == CollisionType.Full ||
                    map[yTile2, xTile1].collisionType == CollisionType.Full ||
                    map[yTile1, xTile2].collisionType == CollisionType.Full ||
                    map[yTile2, xTile2].collisionType == CollisionType.Full)
                {
                    // If character was moving upward, they are still airborne.
                    // Otherwise, they just landed.
                    isAirborne = spriteSpeed.Y <= 0;

                    spritePosition.Y -= (int)(spriteSpeed.Y / Math.Abs(spriteSpeed.Y));
                    spriteSpeed.Y = 0;
                }
            }

            // Allow character to jump for a slight instant after falling off an edge
            if (spriteSpeed.Y > 2)
                isAirborne = true;

            // Check for map collision damage
            xTile1 = (spritePosition.X + 1) / ApplicationGlobals.TILE_SIZE;
            xTile2 = (spritePosition.X + ApplicationGlobals.TILE_SIZE - 2) / ApplicationGlobals.TILE_SIZE;
            yTile1 = spritePosition.Y / ApplicationGlobals.TILE_SIZE;
            yTile2 = (spritePosition.Y + ApplicationGlobals.TILE_SIZE - 1) / ApplicationGlobals.TILE_SIZE;

            if (map[yTile1, xTile1].collisionType == CollisionType.Damage ||
                map[yTile2, xTile1].collisionType == CollisionType.Damage ||
                map[yTile1, xTile2].collisionType == CollisionType.Damage ||
                map[yTile2, xTile2].collisionType == CollisionType.Damage)
            {
                TakeDamage(1);
            }
        }// Move

        virtual public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
                Kill();
            // TODO: Make invulnerable for short time after taking damage
            //else
            //{

            //}
        }

        virtual public void Kill()
        { 
            
        }

        protected bool isAirborne;

        protected float terminalVelocity = 11.0f;
        protected float gravity = 0.4f;

        protected int maxHealth;
        protected int maxMana;
        protected int health;
        protected int mana;

        protected int maxMeleeDamage;
        protected int maxMagicDamage;
        protected int magicRange;
    }
}
