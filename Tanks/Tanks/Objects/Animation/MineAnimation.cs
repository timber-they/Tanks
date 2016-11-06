using System;
using System.Linq;
using Tanks.Objects.GameObjects;

namespace Tanks.Objects.Animation
{
    public class MineAnimation : Animation
    {
        public MineAnimation(GameObject mine, float speed) : base(mine, speed)
        {

        }

        public override void Animate()
        {
            Mine obj;
            if (AnimatedObject == null || (obj = AnimatedObject as Mine) == null)
                return;
            obj.Timer -= Speed / 100;
            obj.View.Shapes.Last().MainColour = (int)Math.Ceiling(obj.Timer) % 2 == 0 ? obj.SpecialColour : obj.NormalColour;
        }
    }
}