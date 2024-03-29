﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rook
{
    public class Hero : Character
    {
        public Hero(int startX = ApplicationGlobals.TileSize, 
                    int startY = 43*ApplicationGlobals.TileSize)
        {
            OldState = Keyboard.GetState();

            Level = 1;
            Experience = LastLevel = 0;
            NextLevel = 100 + 10*Level;
            ShowStats = true;

            SpritePosition = new Rectangle(startX, startY, 
                ApplicationGlobals.TileSize, ApplicationGlobals.TileSize);
            Animation = new Animation(0);
        }

        public override void Load(ContentManager content)
        {
            PTexture = content.Load<Texture2D>("monk");
        }

        public override void Update(GameTime gameTime, MapTile[,] map)
        {
            if (!Exists)
                return;

            NewState = Keyboard.GetState();

            // Show/Hide stats
            if (NewState.IsKeyDown(Keys.S) && !OldState.IsKeyDown(Keys.S))
                ShowStats = !ShowStats;

            UpdateAcceleration();
            UpdateVelocity();
            Animation.UpdateAnimationImage(SpriteSpeed, IsAirborne);
            Move(map, gameTime);

            // TODO: move this logic to relevant area
            if (Experience >= NextLevel)
            {
                Level++;
                LastLevel = NextLevel;
                NextLevel += 100 + 10*Level;

                // TODO: add leveling benefits
            }

            OldState = NewState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            DrawStats(spriteBatch);
        }

        public override void Kill()
        {
            AnimateDeath();

            Exists = false;
            SpritePosition.X = -1000;
            SpritePosition.Y = -1000;
        }

        private void UpdateAcceleration()
        {
            SpriteAcceleration.X = 0;
            SpriteAcceleration.Y = 0;
            SpriteAcceleration.Y += Gravity;

            // Horizontal acceleration
            if (NewState.IsKeyDown(Keys.Left))
                SpriteAcceleration.X -= RunAcceleration;
            else if (SpriteSpeed.X < 0)
                SpriteAcceleration.X += RunAcceleration;

            if (NewState.IsKeyDown(Keys.Right) && !NewState.IsKeyDown(Keys.Left))
                SpriteAcceleration.X += RunAcceleration;
            else if (SpriteSpeed.X > 0)
                SpriteAcceleration.X -= RunAcceleration;

            // Jump
            if (NewState.IsKeyDown(Keys.Up) && !OldState.IsKeyDown(Keys.Up) && !IsAirborne)
            {
                SpriteAcceleration.Y -= JumpAcceleration;
                IsAirborne = true;
            }
        }

        private void UpdateVelocity()
        {
            SpriteSpeed.X += SpriteAcceleration.X;
            SpriteSpeed.Y += SpriteAcceleration.Y;

            if (SpriteSpeed.X > MaxRunSpeed)
                SpriteSpeed.X = MaxRunSpeed;
            else if (SpriteSpeed.X < -1.0f * MaxRunSpeed)
                SpriteSpeed.X = -1.0f * MaxRunSpeed;

            if (SpriteSpeed.Y > TerminalVelocity)
                SpriteSpeed.Y = TerminalVelocity;
            else if (SpriteSpeed.Y < -1.0f * MaxJumpSpeed)
                SpriteSpeed.Y = -1.0f * MaxJumpSpeed;
        }

        // Here lie extremely fragile constants. Sorry.
        private void DrawStats(SpriteBatch spriteBatch)
        {
            if (!ShowStats)
                return;

            // Draw Health/Mana/Exp. Outline
            Rectangle source, destination;
            source.Height = destination.Height = 2*ApplicationGlobals.TileSize;
            source.Width = destination.Width = 4*ApplicationGlobals.TileSize;
            source.X = 0;
            source.Y = 3*ApplicationGlobals.TileSize;

            destination.X = SpritePosition.X - 2*ApplicationGlobals.TileSize + ApplicationGlobals.TileSize/2;
            destination.Y = SpritePosition.Y - 3*ApplicationGlobals.TileSize;

            spriteBatch.Draw(PTexture, destination, source, Color.White);

            // Draw Health
            source.Height = 1;
            source.Width = 14;
            source.X = 0;
            source.Y = 5*ApplicationGlobals.TileSize;

            destination.Height = 1;
            destination.Width = 14;
            destination.X = SpritePosition.X - 2*ApplicationGlobals.TileSize + ApplicationGlobals.TileSize/2 + 2;

            var numRows = (Health*28) / MaxHealth;

            for (var curRow = numRows; curRow > 0; curRow--)
            {
                destination.Y = SpritePosition.Y - 3*ApplicationGlobals.TileSize - curRow + 30;
                spriteBatch.Draw(PTexture, destination, source, Color.White);
            }
            
            // Draw Mana
            source.Y++;
            destination.X += 46;

            numRows = (Mana*28) / MaxMana;

            for (var curRow = numRows; curRow > 0; curRow--)
            {
                destination.Y = SpritePosition.Y - 3*ApplicationGlobals.TileSize - curRow + 30;
                spriteBatch.Draw(PTexture, destination, source, Color.White);
            }

            // Draw Exp.
            source.Y++;
            source.Width = destination.Width = 1;
            source.Height = destination.Height = 4;
            destination.Y = SpritePosition.Y - 3*ApplicationGlobals.TileSize + 14;

            var numCols = ((Experience - LastLevel)*28) / (NextLevel - LastLevel);

            for (var curCol = 0; curCol < numCols; curCol++)
            {
                destination.X = SpritePosition.X - 6 + curCol;
                spriteBatch.Draw(PTexture, destination, source, Color.White);
            }
        }

        private static void AnimateDeath()
        {

        }

        private const float MaxRunSpeed = 2.0f;
        private const float MaxJumpSpeed = 6.0f;
        private const float RunAcceleration = 0.4f;
        private const float JumpAcceleration = 7.0f;

        protected int Level;        // current level
        protected int Experience;   // total experience
        protected int LastLevel;    // experience needed to gain current level
        protected int NextLevel;    // experience needed to level up

        protected bool ShowStats;   // whether or not to draw stat bar

        protected KeyboardState OldState;
        protected KeyboardState NewState;
    }
}
