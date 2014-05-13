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
    public class Hero : Character
    {
        private float maxRunSpeed = 2.0f;
        private float maxJumpSpeed = 6.0f;
        private float runAcceleration = 0.4f;
        private float jumpAcceleration = 6.0f;

        protected int level;        // current level
        protected int experience;   // total experience
        protected int nextLevel;    // experience needed to level up

        protected bool showStats;   // whether or not to draw stat bar

        protected KeyboardState oldState;
        protected KeyboardState newState;

        public Hero()
        {
            oldState = Keyboard.GetState();

            level = 1;
            experience = 0;
            nextLevel = 100 + 10*level;
            showStats = true;

            spritePosition = new Rectangle(ApplicationGlobals.TILE_SIZE, 43 * ApplicationGlobals.TILE_SIZE, 
                ApplicationGlobals.TILE_SIZE, ApplicationGlobals.TILE_SIZE);
            animation.imageSource = new Rectangle(0, 0, ApplicationGlobals.TILE_SIZE, ApplicationGlobals.TILE_SIZE);
        } // ctor

        public override void load(ContentManager Content)
        {
            pTexture = Content.Load<Texture2D>("monk");
        } // load

        public override void Update(GameTime gameTime, MapTile[,] map)
        {
            if (!exists)
                return;

            newState = Keyboard.GetState();

            spriteAcceleration.Y = 0;
            spriteAcceleration.X = 0;

            spriteAcceleration.Y += gravity;

            // Horizontal acceleration
            if (newState.IsKeyDown(Keys.Left))
                spriteAcceleration.X -= runAcceleration;
            else if (spriteSpeed.X < 0)
                spriteAcceleration.X += runAcceleration;

            if (newState.IsKeyDown(Keys.Right) && !newState.IsKeyDown(Keys.Left))
                spriteAcceleration.X += runAcceleration;
            else if (spriteSpeed.X > 0)
                spriteAcceleration.X -= runAcceleration;

            // Jump
            if (newState.IsKeyDown(Keys.Up) && !oldState.IsKeyDown(Keys.Up) && canJump)
            {
                spriteAcceleration.Y -= jumpAcceleration;
                canJump = false;
            }

            // Show/Hide stats
            if (newState.IsKeyDown(Keys.S) && !oldState.IsKeyDown(Keys.S))
                showStats = !showStats;

            spriteSpeed.X += spriteAcceleration.X;
            spriteSpeed.Y += spriteAcceleration.Y;

            if (spriteSpeed.X > maxRunSpeed)
                spriteSpeed.X = maxRunSpeed;
            else if (spriteSpeed.X < -1.0f * maxRunSpeed)
                spriteSpeed.X = -1.0f * maxRunSpeed;

            if (spriteSpeed.Y > terminalVelocity)
                spriteSpeed.Y = terminalVelocity;
            else if (spriteSpeed.Y < -1.0f * maxJumpSpeed)
                spriteSpeed.Y = -1.0f * maxJumpSpeed;

            float xSpeedInt = spriteSpeed.X;
            float ySpeedInt = spriteSpeed.Y;

            UpdateLocation(map, gameTime);
            AnimateHero(newState);

            // TODO: move this logic to relevant area
            if (experience >= nextLevel)
            {
                level++;
                experience -= nextLevel;
                nextLevel = 100 + 10*level;
            }

            oldState = newState;
        } // updateInput

        private void AnimateHero(KeyboardState newState)
        {
            if (newState.IsKeyDown(Keys.Left) && animation.animationSwitch == 0)
                animation.animateSprite = AnimationType.FaceLeft;
            else if (newState.IsKeyDown(Keys.Left) && animation.animationSwitch == 2)
                animation.animateSprite = AnimationType.MoveLeft1;
            else if (newState.IsKeyDown(Keys.Left))
                animation.animateSprite = AnimationType.MoveLeft2;
            else if (newState.IsKeyDown(Keys.Right) && animation.animationSwitch == 0)
                animation.animateSprite = AnimationType.FaceRight;
            else if (newState.IsKeyDown(Keys.Right) && animation.animationSwitch == 2)
                animation.animateSprite = AnimationType.MoveRight1;
            else if (newState.IsKeyDown(Keys.Right))
                animation.animateSprite = AnimationType.MoveRight2;
            else if (animation.animateSprite == AnimationType.MoveLeft1 ||
                     animation.animateSprite == AnimationType.MoveLeft2)
                animation.animateSprite = AnimationType.FaceLeft;
            else if (animation.animateSprite == AnimationType.MoveRight1 ||
                     animation.animateSprite == AnimationType.MoveRight2)
                animation.animateSprite = AnimationType.FaceRight;

            animation.imageSource.X = (int)animation.animateSprite * animation.imageSource.Width;
            animation.animationValue++;

            if (animation.animationValue > animation.animationThreshold)
            {
                animation.animationSwitch++;
                if (animation.animationSwitch > 3)
                    animation.animationSwitch = 0;
                animation.animationValue = 0;
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            drawStats(spriteBatch);
        }

        // Here lie extremely fragile constants. Sorry.
        private void drawStats(SpriteBatch spriteBatch)
        {
            if (!showStats)
                return;

            // Draw Health/Mana/Exp. Outline
            Rectangle source, destination;
            source.Height = destination.Height = 2 * ApplicationGlobals.TILE_SIZE;
            source.Width = destination.Width = 4 * ApplicationGlobals.TILE_SIZE;
            source.X = 7 * ApplicationGlobals.TILE_SIZE;
            source.Y = 0;

            destination.X = spritePosition.X - 2*ApplicationGlobals.TILE_SIZE + ApplicationGlobals.TILE_SIZE/2;
            destination.Y = spritePosition.Y - 3*ApplicationGlobals.TILE_SIZE;

            spriteBatch.Draw(pTexture, destination, source, Color.White);

            // Draw Health
            source.Height = 1;
            source.Width = 14;
            source.X = 7 * ApplicationGlobals.TILE_SIZE;
            source.Y = 2 * ApplicationGlobals.TILE_SIZE;

            destination.Height = 1;
            destination.Width = 14;
            destination.X = spritePosition.X - 2*ApplicationGlobals.TILE_SIZE + ApplicationGlobals.TILE_SIZE/2 + 2;

            int numRows = (health * 28) / maxHealth;

            for (int curRow = numRows; curRow > 0; curRow--)
            {
                destination.Y = spritePosition.Y - 3*ApplicationGlobals.TILE_SIZE - curRow + 30;
                spriteBatch.Draw(pTexture, destination, source, Color.White);
            }
            
            // Draw Mana
            source.Y++;
            destination.X += 46;

            numRows = (mana * 28) / maxMana;

            for (int curRow = numRows; curRow > 0; curRow--)
            {
                destination.Y = spritePosition.Y - 3*ApplicationGlobals.TILE_SIZE - curRow + 30;
                spriteBatch.Draw(pTexture, destination, source, Color.White);
            }

            // Draw Exp.
            source.Y++;
            source.Width = destination.Width = 1;
            source.Height = destination.Height = 4;
            destination.Y = spritePosition.Y - 3*ApplicationGlobals.TILE_SIZE + 14;

            int numCols = (experience * 28) / nextLevel;

            for (int curCol = 0; curCol < numCols; curCol++)
            {
                destination.X = spritePosition.X - 6 + curCol;
                spriteBatch.Draw(pTexture, destination, source, Color.White);
            }
        } // drawStats

        public void animateDamage()
        {

        } // animateDamage

        public void animateDeath()
        {

        } // animateDeath

        public override void kill()
        {
            animateDeath();

            exists = false;
            spritePosition.X = 0;
            spritePosition.Y = 0;
        } // kill
    }
}
