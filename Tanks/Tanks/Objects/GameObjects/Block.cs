using System.Collections.ObjectModel;
using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class Block : GameObject
    {
        // ReSharper disable once NotAccessedField.Global
        public bool Destroyable;

        public Block(Coordinate position, Coordinate unturnedSiz, decimal id, bool destroyable, Colour mainColor, float rotation = 0)
            : base(
                position, unturnedSiz, rotation,
                new ShapeCollection(new ObservableCollection<Shape>
                {
                    new Painting.Types.Paint.Rectangle(2, new Colour(Color.FromArgb(-8355712)), new Coordinate(725, 354), new Coordinate(100, 100), mainColor, 0)
                }) {Position = new Coordinate(0, 0)}, id)
        {
            Destroyable = destroyable;
        }
    }
}