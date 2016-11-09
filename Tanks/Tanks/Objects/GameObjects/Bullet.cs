using System.Collections.ObjectModel;
using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class Bullet : GameObject
    {
        protected Bullet(Coordinate position, Coordinate unturnedSize, Colour colour, float rotation, decimal id,
            int availableCollisionCount)
            : base(
                position, unturnedSize, rotation,
                new ShapeCollection(new ObservableCollection<Shape>
                {
                    new Ellipse(0, new Colour(Color.Empty), new Coordinate(644, 380), new Coordinate(50, 120),
                        new Colour(Color.FromArgb(-16777216)), 90),
                    new Polygon(3, 0, new Colour(Color.Empty), new Coordinate(600, 389), new Coordinate(70, 100),
                        new Colour(Color.FromArgb(-65536)), 90, 30)
                }) {Position = new Coordinate(0, 0)}, id)
        {
            View.Shapes[1].MainColour = colour;
            AvailableCollisionCount = availableCollisionCount;
        }

        public int AvailableCollisionCount { get; set; }
    }
}