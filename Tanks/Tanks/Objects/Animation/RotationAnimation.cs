using Tanks.Backend;
using Tanks.Objects.GameObjects;

namespace Tanks.Objects.Animation
{
    public class RotationAnimation : Animation
    {
        public float Aim { get; }
        public RotationAnimation(GameObject animatedObject, float speed, float aim) : base(animatedObject, speed)
        {
            Aim = aim;
        }

        public override void Animate(InGameEngine engine)
        {
            if (AnimatedObject == null)
                return;
            AnimatedObject.Rotation = Arithmetic.RealAngle(AnimatedObject.Rotation + Speed/* * (Aim - AnimatedObject.Rotation < 180 ? 1 : -1)*/);
        }
    }
}