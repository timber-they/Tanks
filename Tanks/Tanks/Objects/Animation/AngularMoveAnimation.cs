using System;
using Painting.Types.Paint;
using Painting.Util;
using Tanks.Backend;
using Tanks.Objects.GameObjects;

namespace Tanks.Objects.Animation
{
    public class AngularMoveAnimation : Animation
    {
        public AngularMoveAnimation(GameObject animatedObject, float direction, float speed)
            : base(animatedObject, speed)
        {
            Direction = direction;
        }

        private float Direction { get; set; }

        public override void Animate(InGameEngine engine)
        {
            if (AnimatedObject == null)
                return;
            AnimatedObject.Position =
                AnimatedObject.Position.Add(new Coordinate((float) Math.Cos(Physomatik.ToRadian(Direction))*Speed,
                    (float) Math.Sin(Physomatik.ToRadian(Direction))*Speed));
        }
    }
}