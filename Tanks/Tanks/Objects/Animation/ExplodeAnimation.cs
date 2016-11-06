using System;
using Painting.Types.Paint;
using Tanks.Objects.GameObjects;

namespace Tanks.Objects.Animation
{
    public class ExplodeAnimation : Animation
    {
        public ExplodeAnimation(GameObject explosion, float speed, Coordinate maxSize) : base(explosion, speed)
        {
            MaxSize = maxSize;
        }

        public override void Animate()
        {
            Explosion obj;
            if (AnimatedObject == null || (obj = AnimatedObject as Explosion) == null)
                return;
            AnimatedObject.ChangeSizeAtCentre(AnimatedObject.Size.Add(Speed));
            if(obj.Destroying)
            {
                //Todo Destroying Explosions (from mines) have not been implemented yet - destroy the stuff!
            }
        }

        public Coordinate MaxSize { get; set; }
    }
}