using System.Drawing;
using System.Linq;
using Painting.Types.Paint;
using Tanks.Backend;
using Tanks.Enums;

namespace Tanks.Objects
{
    public class Animation
    {
        public decimal AnimatedObjectId { get; set; }
        public Direction Direction { get; set; }
        public InGameEngine Engine { get; set; }

        public Animation (decimal animatedObjectId, Direction direction, InGameEngine engine)
        {
            AnimatedObjectId = animatedObjectId;
            Direction = direction;
            Engine = engine;
        }

        public void AnimateMovement (Graphics p)
        {
            var obj = Engine.Field.Objects.FirstOrDefault(o => o.Id == AnimatedObjectId);
            if (obj == null)
                return;
            switch (Direction)
            {
                case Direction.Right:
                    obj.Position = obj.Position.Add(new Coordinate(2, 0));
                    break;
                case Direction.Down:
                    obj.Position = obj.Position.Add(new Coordinate(0, 2));
                    break;
                case Direction.Left:
                    obj.Position = obj.Position.Add(new Coordinate(-2, 0));
                    break;
                case Direction.Up:
                    obj.Position = obj.Position.Add(new Coordinate(0, -2));
                    break;
            }
        }
    }
}