using System.Collections.ObjectModel;
using System.Drawing;
using System.Security.Claims;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class Block : GameObject
    {
        public bool Destroyable;

        public Block(Coordinate position, Coordinate size, decimal id, bool destroyable, float rotation = 0)
            : base(
                position, size, rotation,
                new ShapeCollection(new ObservableCollection<Shape>
                {
                    new Polygon(4, 2, new Colour(Color.FromArgb(-8355712)), new Coordinate(725, 354),
                        new Coordinate(100, 100), new Colour(Color.FromArgb(-4144960)), 0, 45),
                }) {Position = new Coordinate(0, 0)}, id)
        {
            Destroyable = destroyable;
        }
    }
}