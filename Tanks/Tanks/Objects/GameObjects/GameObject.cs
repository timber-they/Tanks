using System;
using Painting.Types.Paint;
using Tanks.Enums;
using Tanks.Types;

namespace Tanks.Objects.GameObjects
{
    public class GameObject
    {
        public readonly decimal Id;
        private Coordinate _position;
        private float _rotation;
        private Coordinate _unturnedSize;
        private ShapeCollection _view;

        protected GameObject(Coordinate position, Coordinate unturnedSize, float rotation, ShapeCollection view, decimal id)
        {
            Id = id;
            if (!(this is Field))
                View = view;
            Position = position;
            UnturnedSize = unturnedSize;
            Rotation = rotation;
            if (this is Field)
                View = view;
        }

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                if ((View != null) && (Math.Abs(View.Rotation - value) > 0.001))
                    View.Rotation = value;
                _rotation = value;
            }
        }

        public Coordinate Position
        {
            get { return _position; }
            set
            {
                if ((View != null) && ((View.Position == null) || !View.Position.Equals(value)))
                    View.Position = value;
                _position = value;
            }
        }

        public Coordinate UnturnedSize
        {
            get { return _unturnedSize; }
            private set
            {
                _unturnedSize = value;
                if ((View != null) && ((View.UnturnedSize == null) || !View.UnturnedSize.Equals(value)))
                    View.UnturnedSize = value;
            }
        }

        public ShapeCollection View //Don't change the view - always change the Object itself!
        {
            get { return _view; }
            protected set
            {
                if ((View != null) && View.Equals(value))
                    return;
                _view = value;
            }
        }

        public void ChangeSizeAtCentre(Coordinate newSize)
        {
            Position = Position.Sub(newSize.Sub(UnturnedSize).Div(2));
            UnturnedSize = newSize;
        }

        public Direction Cuts(GameObject other)
        {
            var cuttingX = Cuts(Position.X, UnturnedSize.X, other.Position.X, other.UnturnedSize.X, true);
            var cuttingY = Cuts(Position.Y, UnturnedSize.Y, other.Position.Y, other.UnturnedSize.Y, false);
            return (cuttingX.Direction == Direction.Nothing) || (cuttingY.Direction == Direction.Nothing)
                ? Direction.Nothing
                : (cuttingX.Float < cuttingY.Float ? cuttingX.Direction : cuttingY.Direction);
        }

        private static DirectionFloat Cuts(float pos0, float size0, float pos1, float size1, bool x) //From Direction
        {
            if ((pos0 + size0 > pos1) && (pos0 + size0 < pos1 + size1))
                return new DirectionFloat(x ? Direction.Right : Direction.Down, pos0 + size0 - pos1);
            if ((pos1 + size1 > pos0) && (pos1 + size1 < pos0 + size0))
                return new DirectionFloat(x ? Direction.Left : Direction.Up, pos1 + size1 - pos0);
            return new DirectionFloat(Direction.Nothing, -1);
        }

        public Coordinate CenterPosition() => Position.Add(UnturnedSize.Div(2));
    }
}