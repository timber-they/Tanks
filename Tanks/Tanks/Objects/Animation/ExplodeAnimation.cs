using Painting.Types.Paint;
using Tanks.Backend;
using Tanks.Objects.GameObjects;

namespace Tanks.Objects.Animation
{
    public class ExplodeAnimation : Animation
    {
        public ExplodeAnimation(GameObject explosion, float speed, Coordinate maxSize) : base(explosion, speed)
        {
            MaxSize = maxSize;
        }

        public override void Animate(InGameEngine engine)
        {
            Explosion obj;
            if (AnimatedObject == null || (obj = AnimatedObject as Explosion) == null)
                return;
            AnimatedObject.ChangeSizeAtCentre(AnimatedObject.UnturnedSize.Add(Speed));
            if(obj.Destroying)
            {
                //Todo Destroying Explosions (from mines) have not been implemented yet - destroy the stuff!
            }
        }

        public Coordinate MaxSize { get; }
    }
}