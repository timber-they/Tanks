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

        public override void Animate() => AnimatedObject?.ChangeSizeAtCentre(AnimatedObject.Size.Add(Speed));

        public Coordinate MaxSize { get; set; }
    }
}