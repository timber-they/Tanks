using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects
{
    public class Bullet : GameObject
    {
        public Bullet(Coordinate position, Coordinate size, Colour colour, float rotation, decimal id)
            : base(
                position, size, rotation,
                new ShapeCollection(new ObservableCollection<Shape>
                {
                    new Ellipse(0, new Colour(Color.Empty), new Coordinate(658, 277), new Coordinate(50, 120),
                        new Colour(Color.FromArgb(-16777216)), 0f),
                    new Polygon(3, 0, new Colour(Color.Empty), new Coordinate(648, 322), new Coordinate(70, 100),
                        new Colour(Color.FromArgb(-65536)), 0, 30),
                }) {Position = new Coordinate(0, 0)}, id)
        {
            View.Shapes[1].MainColour = colour;
        }
    }
}