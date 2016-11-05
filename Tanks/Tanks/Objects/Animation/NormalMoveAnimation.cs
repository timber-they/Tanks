using Painting.Types.Paint;
using Tanks.Enums;
using Tanks.Objects.GameObjects;

namespace Tanks.Objects.Animation
{
    public class NormalMoveAnimation : Animation
    {
        public NormalMoveAnimation(GameObject animatedObject, Direction direction, float speed)
            : base(animatedObject, speed)
        {
            Direction = direction;
        }

        public Direction Direction { get; set; }

        public override void AnimateMovement()
        {
            if (AnimatedObject == null)
                return;
            switch (Direction)
            {
                case Direction.Right:
                    AnimatedObject.Position = AnimatedObject.Position.Add(new Coordinate(Speed, 0));
                    break;
                case Direction.Down:
                    AnimatedObject.Position = AnimatedObject.Position.Add(new Coordinate(0, Speed));
                    break;
                case Direction.Left:
                    AnimatedObject.Position = AnimatedObject.Position.Add(new Coordinate(-Speed, 0));
                    break;
                case Direction.Up:
                    AnimatedObject.Position = AnimatedObject.Position.Add(new Coordinate(0, -Speed));
                    break;
            }
        }
    }
}