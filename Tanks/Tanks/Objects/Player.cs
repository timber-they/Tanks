using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects
{
    public class Player : GameObject
    {
        private float _rotation;

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                View.Rotation = value;
                _rotation = value;
            }
        }

        public int Lives { get; set; }

        public Player(float rotation, int lives, Coordinate position, Coordinate size, Colour colour)
            : base(
                position, size,
                new ShapeCollection(new ObservableCollection<Shape>
                {
                    new Line(new Coordinate(525, 319), new Coordinate(525, 227.5786f),
                        new Colour(Color.FromArgb(-16711936)), 7),
                    new Polygon(4, 2, new Colour(Color.FromArgb(-65536)), new Coordinate(468, 262),
                        new Coordinate(116, 116), new Colour(Color.Empty), 45),
                    new Ellipse(0, new Colour(Color.Empty), new Coordinate(475, 270), new Coordinate(100, 100),
                        new Colour(Color.FromArgb(-16777216)), 0f),
                }) {Position = new Coordinate(0, 0)})
        {
            Rotation = rotation;
            Lives = lives;
            View.Shapes[0].MainColour = new Colour(Color.FromArgb(colour.Color.ToArgb() * 3));
            View.Shapes[1].MainColour = colour;
        }
    }
}