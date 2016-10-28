using System;
using Painting.Types.Paint;

namespace Tanks.Objects
{
    public class GameObject
    {
        private Coordinate _position;
        private Coordinate _size;
        private ShapeCollection _view;
        private float _rotation;

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
                if (View != null && (View.Size == null || !View.Size.Equals (value)))
                    View.Size = value;
                _size = value;
            }
        }

        public ShapeCollection View
        {
            get { return _view; }
            set
            {
                if (View != null && View.Equals(value))
                    return;
                if (Size == null || !Size.Equals (value.Size))
                    Size = value.Size;
                if (Position == null || !Position.Equals (value.Position))
                    Position = value.Position;
                if (Math.Abs(Rotation - value.Rotation) > 0.001)
                    Rotation = value.Rotation;
                _view = value;
            }
        }

        public GameObject (Coordinate position, Coordinate size, float rotation ,ShapeCollection view)
        {
            if (!(this is Field))
                View = view;
            Position = position;
            Size = size;
            Rotation = rotation;
            if (this is Field)
                View = view;
        }
    }
}