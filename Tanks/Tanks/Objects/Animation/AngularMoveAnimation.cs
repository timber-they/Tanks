using System;
using System.Linq;
using Painting.Types.Paint;
using Painting.Util;
using Tanks.Backend;
using Tanks.Objects.GameObjects;

namespace Tanks.Objects.Animation
{
    public class AngularMoveAnimation : Animation
    {
        public float Direction { get; set; }

        public AngularMoveAnimation(GameObject animatedObject, float direction, InGameEngine engine, float speed)
            : base(animatedObject, engine, speed)
        {
            Direction = direction;
        }

        public override void AnimateMovement()
        {
            if(AnimatedObject == null)
                return;
            AnimatedObject.Position = AnimatedObject.Position.Add(new Coordinate((float) Math.Cos(Physomatik.ToRadian(Direction)) * Speed,
                    (float) Math.Sin(Physomatik.ToRadian(Direction)) * Speed));
        }
    }
}