using System.Drawing;
using System.Linq;
using Painting.Types.Paint;
using Tanks.Backend;

namespace Tanks.Objects
{
    public class Animation
    {
        public decimal AnimatedObjectId { get; set; }
        public Coordinate Destination { get; set; }
        public InGameEngine Engine { get; set; }

        public Animation (decimal animatedObjectId, Coordinate destination, InGameEngine engine)
        {
            AnimatedObjectId = animatedObjectId;
            Destination = destination;
            Engine = engine;
        }

        public bool AnimateMovement(Graphics p)
        {
            var obj = Engine.Field.Objects.FirstOrDefault(o => o.Id == AnimatedObjectId);
            if (obj == null)
                return false;
            var dif = Destination.Sub(obj.Position);
            var delta = Destination.Dif(obj.Position);
            if (delta.Is0)
                return false;
            if (delta.X > 0)
                obj.Position =
                    delta.X < 1
                        ? obj.Position = new Coordinate(Destination.X, obj.Position.Y)
                        : dif.X > 0
                            ? obj.Position = new Coordinate(obj.Position.X + 1, obj.Position.Y)
                            : obj.Position = new Coordinate(obj.Position.X - 1, obj.Position.Y);
            if (delta.Y > 0)
                obj.Position =
                    delta.Y < 1
                        ? obj.Position = new Coordinate(obj.Position.X, Destination.Y)
                        : dif.Y > 0
                            ? obj.Position = new Coordinate(obj.Position.X, obj.Position.Y + 1)
                            : obj.Position = new Coordinate(obj.Position.X, obj.Position.Y - 1);
            return true;
        }
    }
}