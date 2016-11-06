using System.Collections.ObjectModel;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class Explosion : GameObject
    {
        public Explosion(Coordinate position, Coordinate size, decimal id, Colour color, float width = 2,
            float rotation = 0) : base(position, size, rotation, new ShapeCollection(new ObservableCollection<Shape>
        {
            new Ellipse(width, color, new Coordinate(0, 0), size, Colour.Invisible(), 0)
        }), id)
        {
        }
    }
}