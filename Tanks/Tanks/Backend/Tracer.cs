using Painting.Types.Paint;
using Tanks.Objects;
using Tanks.Objects.GameObjects;

namespace Tanks.Backend
{
    public class Tracer
    {
        public static void TraceMouse(Coordinate mousePosition, GameObject obj)
        {
            var dif = obj.Position.Add(obj.Size.Div(2)).Sub(mousePosition);
            var angle = dif.Atan+ (dif.X > 0 ? 180 : 0);
            obj.Rotation = angle;
        }
    }
}