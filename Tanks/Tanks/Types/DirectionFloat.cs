using Tanks.Enums;

namespace Tanks.Types
{
    public class DirectionFloat
    {
        public Direction Direction { get; set; }
        public float Float { get; set; }

        public DirectionFloat(Direction direction, float f)
        {
            Direction = direction;
            Float = f;
        }
    }
}