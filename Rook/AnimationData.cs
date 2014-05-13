using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Rook
{
    public class AnimationData
    {
        public AnimationData()
        {
            animateSprite = AnimationType.Default;
            animationSwitch = 0;
            animationValue = 0;
            animationThreshold = 3;
            imageSource = new Rectangle(0, 0, ApplicationGlobals.TILE_SIZE, ApplicationGlobals.TILE_SIZE);
        }

        public AnimationType animateSprite;  // Used to determine current animation image to display
        public int animationSwitch;          // Current animation decision
        public int animationValue;           // Increased each 'tick'; switch animation image when reaches threshold
        public int animationThreshold;       // See above (value and threshold used for timing the animations)
        public Rectangle imageSource;        // Where to get image to display from spritesheet
    }
}
