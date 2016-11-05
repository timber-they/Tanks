using System.Collections.ObjectModel;
using System.Drawing;
using Painting.Types.Paint;
using Rectangle = Painting.Types.Paint.Rectangle;

namespace Tanks.Objects.GameObjects
{
    public class Player : GameObject
    {
        protected Player(float rotation, int lives, Coordinate position, Coordinate size, Colour colour, decimal id)
            : base(
                position, size, rotation,
                new ShapeCollection(new ObservableCollection<Shape>
                {
                    new Line(new Coordinate(57, 57), new Coordinate(149, 57), new Colour(Color.FromArgb(-16711936)), 7),
                    new Rectangle(2, new Colour(Color.FromArgb(-65536)), new Coordinate(17, 15), new Coordinate(84, 84),
                        new Colour(Color.Empty), 0),
                    new Ellipse(0, new Colour(Color.Empty), new Coordinate(7, 8), new Coordinate(100, 100),
                        new Colour(Color.FromArgb(-16777216)), 0)
                }) {Position = new Coordinate(0, 0)}, id)
        {
            Lives = lives;
            View.Shapes[0].MainColour = new Colour(Color.FromArgb(colour.Color.ToArgb()*3));
            var polygon = View.Shapes[1] as Rectangle;
            if (polygon != null)
                polygon.LineColour = colour;
        }

        public int Lives { get; set; }
    }
}