using System;

namespace Tanks.Enums
{
    public enum Direction
    {
        Right,
        Down,
        Left,
        Up,
        Nothing
    }

    public static class DirectionFunctionality
    {
        public static Direction Inverted(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return Direction.Left;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Up:
                    return Direction.Down;
                case Direction.Nothing:
                    return Direction.Nothing;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }


        public static Direction Next(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Up;
                case Direction.Up:
                    return Direction.Right;
                case Direction.Nothing:
                    return Direction.Nothing;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}