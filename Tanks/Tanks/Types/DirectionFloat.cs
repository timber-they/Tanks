using Tanks.Enums;

namespace Tanks.Types
{
    public class DirectionFloat
    {
        public DirectionFloat(Direction direction, float f)
        {
            Direction = direction;
            Float = f;
        }

        public Direction Direction { get; }
        public float Float { get; }
    }
}