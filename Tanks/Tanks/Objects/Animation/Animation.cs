using Tanks.Backend;
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

        public GameObject AnimatedObject { get; }
        public float Speed { get; }

        public virtual void Animate(InGameEngine engine)
        {
        }
    }
}