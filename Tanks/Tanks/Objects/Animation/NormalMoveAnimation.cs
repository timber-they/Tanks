using System.Drawing;
using System.Linq;
using Painting.Types.Paint;
using Tanks.Backend;
using Tanks.Enums;
using Tanks.Objects.GameObjects;

namespace Tanks.Objects.Animation
{
    public class NormalMoveAnimation : Animation
    {
        public Direction Direction { get; set; }

        public NormalMoveAnimation (GameObject animatedObject, Direction direction, InGameEngine engine, float speed) : base(animatedObject, engine, speed)
        {
            AnimatedObject = animatedObject;
            Direction = direction;
            Engine = engine;
        }

        public override void AnimateMovement ()
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