using System;
using Painting.Types.Paint;
using Tanks.Enums;
using Tanks.Types;

namespace Tanks.Objects.GameObjects
{
    public class GameObject
    {
        private Coordinate _position;
        private Coordinate _size;
        private ShapeCollection _view;
        private float _rotation;
        public readonly decimal Id;

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                if (View != null && Math.Abs(View.Rotation - value) > 0.001)
                    View.Rotation = value;
                _rotation = value;
            }
        }

        public Coordinate Position
        {
            get { return _position; }
            set
            {
                if (View != null && (View.Position == null || !View.Position.Equals (value)))
                    View.Position = value;
                _position = value;
            }
        }

        public Coordinate Size
        {
            get { return _size; }
            set
            {
                _size = value;
                if (View != null && (View.Size == null || !View.Size.Equals (value)))
                    View.Size = value;
            }
        }

        public ShapeCollection View //Don't change the view - always change the Object itself!
        {
            get { return _view; }
            set
            {
                if (View != null && View.Equals(value))
                    return;
                _view = value;
            }
        }

        public GameObject (Coordinate position, Coordinate size, float rotation, ShapeCollection view, decimal id)
        {
            Id = id;
            if (!(this is Field))
                View = view;
            Position = position;
            Size = size;
            Rotation = rotation;
            if (this is Field)
                View = view;
        }

        public Direction Cuts(GameObject other)
        {
            var cuttingx = Cuts(Position.X, Size.X, other.Position.X, other.Size.X, true);
            var cuttingy = Cuts(Position.Y, Size.Y, other.Position.Y, other.Size.Y, false);
            return cuttingx.Direction == Direction.Nothing || cuttingy.Direction == Direction.Nothing
                ? Direction.Nothing
                : (cuttingx.Float < cuttingy.Float ? cuttingx.Direction : cuttingy.Direction);
        }

        public DirectionFloat Cuts (float pos0, float size0, float pos1, float size1, bool x) //From Direction
        {
            if (pos0 + size0 > pos1 && pos0 + size0 < pos1 + size1)
                return new DirectionFloat(x ? Direction.Right : Direction.Down, pos0 + size0 - pos1);
            if (pos1 + size1 > pos0 && pos1 + size1 < pos0 + size0)
                return new DirectionFloat(x ? Direction.Left : Direction.Up, pos1 + size1 - pos0);
            return new DirectionFloat(Direction.Nothing, -1);
        }
    }
}