using Painting.Types.Paint;
using Tanks.Objects.GameObjects;

namespace Tanks.Backend
{
    public static class Tracer
    {
        public static float TracePosition(Coordinate position, GameObject obj)
        {
            var dif = obj.Position.Add(obj.Size.Div(2)).Sub(position);
            var angle = dif.Atan() + (dif.X >= 0 ? 180 : 0);
            return angle;
        }
    }
}