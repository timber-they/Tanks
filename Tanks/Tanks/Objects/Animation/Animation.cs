using Tanks.Objects.GameObjects;

namespace Tanks.Objects.Animation
{
    public class Animation
    {
        protected Animation(GameObject animatedObject, float speed)
        {
            AnimatedObject = animatedObject;
            Speed = speed;
        }

        public GameObject AnimatedObject { get; set; }
        protected float Speed { get; set; }

        public virtual void Animate()
        {
        }
    }
}