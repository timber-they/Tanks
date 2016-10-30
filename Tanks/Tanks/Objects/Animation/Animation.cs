using Tanks.Backend;

namespace Tanks.Objects.Animation
{
    public class Animation
    {
        public GameObject AnimatedObject { get; set; }
        public InGameEngine Engine { get; set; }
        public float Speed { get; set; }

        public Animation(GameObject animatedObject, InGameEngine engine, float speed)
        {
            AnimatedObject = animatedObject;
            Engine = engine;
            Speed = speed;
        }

        public virtual void AnimateMovement()
        {
            
        }
    }
}