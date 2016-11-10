using System.Collections.ObjectModel;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class Explosion : GameObject
    {
        public Explosion(Coordinate position, Coordinate size, decimal id, Colour lineColor, Colour mainColor, bool destroying, float width = 2,
            float rotation = 0) : base(position, size, rotation, new ShapeCollection(new ObservableCollection<Shape>
        {
            new Ellipse(width, lineColor, new Coordinate(0, 0), size, mainColor, 0)
        }), id)
        {
            Destroying = destroying;
        }

        public bool Destroying { get; }
    }
}