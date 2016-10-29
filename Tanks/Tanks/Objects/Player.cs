using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects
{
    public class Player : GameObject
    {
        public int Lives { get; set; }

        public Player(float rotation, int lives, Coordinate position, Coordinate size, Colour colour, decimal id)
            : base(
                position, size, rotation,
                new ShapeCollection(new ObservableCollection<Shape>
                {
                    new Line(new Coordinate(525, 319), new Coordinate(525, 227.5786f),
                        new Colour(Color.FromArgb(-16711936)), 7),
                    new Polygon(4, 2, new Colour(Color.FromArgb(-65536)), new Coordinate(468, 262),
                        new Coordinate(116, 116), new Colour(Color.Empty), 0, 45),
                    new Ellipse(0, new Colour(Color.Empty), new Coordinate(475, 270), new Coordinate(100, 100),
                        new Colour(Color.FromArgb(-16777216)), 0f),
                }) {Position = new Coordinate(0, 0)}, id)
        {
            Lives = lives;
            View.Shapes[0].MainColour = new Colour(Color.FromArgb(colour.Color.ToArgb() * 3));
            var polygon = View.Shapes[1] as Polygon;
            if (polygon != null)
                polygon.LineColour = colour;
        }
    }
}