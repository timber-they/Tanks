using System.Collections.ObjectModel;
using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class Hole : GameObject
    {
        public Hole(Coordinate position, Coordinate size, decimal id, float rotation = 0)
            : base(
                position, size, rotation,
                new ShapeCollection(new ObservableCollection<Shape>
                {
                    new Ellipse(2, new Colour(Color.FromArgb(-12582912)), new Coordinate(418, 221),
                        new Coordinate(100, 100), new Colour(Color.FromArgb(-14013910)), 0)
                }) {Position = new Coordinate(0, 0)}, id)
        {
        }
    }
}