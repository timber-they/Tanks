using System.Collections.ObjectModel;
using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class Mine : GameObject
    {
        public Mine(Coordinate position, Coordinate unturnedSiz, decimal id, float time, Colour normalColour, Colour specialColour, Coordinate explosionSize, float rotation = 0)
            : base(
                position, unturnedSiz, rotation,
                new ShapeCollection(new ObservableCollection<Shape>
                {
                    new Ellipse(1, new Colour(Color.Empty), new Coordinate(706, 242), new Coordinate(68, 68),
                        new Colour(Color.FromArgb(-128)), 0),
                    new Ellipse(1, new Colour(Color.FromArgb(-4144960)), new Coordinate(689, 226),
                        new Coordinate(100, 100), normalColour, 0),
                })
                { Position = new Coordinate(0, 0) }, id)
        {
            Timer = time;
            NormalColour = normalColour;
            SpecialColour = specialColour;
            ExplosionSize = explosionSize;
        }

        public float Timer { get; set; }
        public Colour NormalColour { get; }
        public Colour SpecialColour { get; }
        public Coordinate ExplosionSize { get; }
    }
}