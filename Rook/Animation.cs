using Microsoft.Xna.Framework;

namespace Rook
{
    public class Animation
    {
        public Animation(int spriteIndex, int framesPerImage = 3)
        {
            State = AnimationState.Default;
            CurrentImageFrame = 0;
            FramesPerImage = framesPerImage;
            ImageSource = new Rectangle(0, spriteIndex*ApplicationGlobals.TILE_SIZE, 
                ApplicationGlobals.TILE_SIZE, ApplicationGlobals.TILE_SIZE);
        }

        public void UpdateAnimationImage(Vector2 spriteSpeed, bool isAirborne)
        {
            if (CurrentImageFrame > FramesPerImage)
            {
                CurrentImageFrame = 0;

                if (spriteSpeed.X < -1)
                {
                    switch (State)
                    {
                        case AnimationState.MoveLeft1:
                            State = AnimationState.MoveLeft2;
                            break;
                        case AnimationState.MoveLeft2:
                            State = AnimationState.MoveLeft3;
                            break;
                        case AnimationState.MoveLeft3:
                            State = AnimationState.MoveLeft4;
                            break;
                        default:
                            State = AnimationState.MoveLeft1;
                            break;
                    }
                }
                else if (spriteSpeed.X > 1)
                {
                    switch (State)
                    {
                        case AnimationState.MoveRight1:
                            State = AnimationState.MoveRight2;
                            break;
                        case AnimationState.MoveRight2:
                            State = AnimationState.MoveRight3;
                            break;
                        case AnimationState.MoveRight3:
                            State = AnimationState.MoveRight4;
                            break;
                        default:
                            State = AnimationState.MoveRight1;
                            break;
                    }
                }
                else
                {
                    switch (State)
                    {
                        // Facing Left
                        case AnimationState.FaceLeft:
                        case AnimationState.MoveLeft1:
                        case AnimationState.MoveLeft2:
                        case AnimationState.MoveLeft3:
                        case AnimationState.MoveLeft4:
                            State = isAirborne ? AnimationState.MoveLeft4 : AnimationState.FaceLeft;
                            break;
                        // Facing Right
                        default:
                            State = isAirborne ? AnimationState.MoveRight4 : AnimationState.FaceRight;
                            break;
                    }
                }
            }

            ImageSource.X = (int)State * ImageSource.Width;
            CurrentImageFrame++;
        }

        public AnimationState State;    // Used to determine current animation image to display
        public int CurrentImageFrame;   // Increased each 'tick'; switch animation image when reaches threshold
        public int FramesPerImage;      // See above (value and threshold used for timing the animations)
        public Rectangle ImageSource;   // Where to get image to display from spritesheet
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
