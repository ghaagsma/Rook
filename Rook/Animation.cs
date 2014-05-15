using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Rook
{
    public class Animation
    {
        public Animation(int spriteIndex, int _framesPerImage = 3)
        {
            state = AnimationState.Default;
            currentImageFrame = 0;
            framesPerImage = _framesPerImage;
            imageSource = new Rectangle(0, spriteIndex * ApplicationGlobals.TILE_SIZE, 
                ApplicationGlobals.TILE_SIZE, ApplicationGlobals.TILE_SIZE);
        }

        public void UpdateAnimationImage(Vector2 spriteSpeed, bool isAirborne)
        {

            if (currentImageFrame > framesPerImage)
            {
                currentImageFrame = 0;

                if (spriteSpeed.X < -1)
                {
                    switch (state)
                    {
                        case AnimationState.MoveLeft1:
                            state = AnimationState.MoveLeft2;
                            break;
                        case AnimationState.MoveLeft2:
                            state = AnimationState.MoveLeft3;
                            break;
                        case AnimationState.MoveLeft3:
                            state = AnimationState.MoveLeft4;
                            break;
                        default:
                            state = AnimationState.MoveLeft1;
                            break;
                    }
                }
                else if (spriteSpeed.X > 1)
                {
                    switch (state)
                    {
                        case AnimationState.MoveRight1:
                            state = AnimationState.MoveRight2;
                            break;
                        case AnimationState.MoveRight2:
                            state = AnimationState.MoveRight3;
                            break;
                        case AnimationState.MoveRight3:
                            state = AnimationState.MoveRight4;
                            break;
                        default:
                            state = AnimationState.MoveRight1;
                            break;
                    }
                }
                else
                {
                    switch (state)
                    {
                        case AnimationState.FaceLeft:
                        case AnimationState.MoveLeft1:
                        case AnimationState.MoveLeft2:
                        case AnimationState.MoveLeft3:
                        case AnimationState.MoveLeft4:
                            if (isAirborne)
                                state = AnimationState.MoveLeft4;
                            else
                                state = AnimationState.FaceLeft;
                            break;
                        case AnimationState.FaceRight:
                        case AnimationState.MoveRight1:
                        case AnimationState.MoveRight2:
                        case AnimationState.MoveRight3:
                        case AnimationState.MoveRight4:
                        default:
                            if (isAirborne)
                                state = AnimationState.MoveRight4;
                            else
                                state = AnimationState.FaceRight;
                            break;
                    }
                }
            }

            imageSource.X = (int)state * imageSource.Width;
            currentImageFrame++;
        }

        public AnimationState state;    // Used to determine current animation image to display
        public int currentImageFrame;   // Increased each 'tick'; switch animation image when reaches threshold
        public int framesPerImage;      // See above (value and threshold used for timing the animations)
        public Rectangle imageSource;   // Where to get image to display from spritesheet
    }

    public enum AnimationState
    {
        Default = 0,
        FaceLeft = 1,
        MoveLeft1 = 2,
        MoveLeft2 = 3,
        MoveLeft3 = 4,
        MoveLeft4 = 5,
        FaceRight = 6,
        MoveRight1 = 7,
        MoveRight2 = 8,
        MoveRight3 = 9,
        MoveRight4 = 10,
        TakeDamage = 11,
        GiveDamage = 12,
        Die = 13,
        Dead = 14
    };
}
