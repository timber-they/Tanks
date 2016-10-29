using Painting.Types.Paint;
using Tanks.Objects;

namespace Tanks.Backend
{
    public class Tracer
    {
        public static void TraceMouse(Coordinate mousePosition, GameObject obj)
        {
            var dif = obj.Position.Sub(mousePosition);
            var angle = dif.Atan+ (dif.X > 0 ? 180 : 0);
            obj.Rotation = angle + 90;
        }
    }
}