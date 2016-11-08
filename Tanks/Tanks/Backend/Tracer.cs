using Painting.Types.Paint;
using Tanks.Objects.GameObjects;

namespace Tanks.Backend
{
    public static class Tracer
    {
        public static void TracePosition(Coordinate position, GameObject obj)
        {
            var dif = obj.Position.Add(obj.UnturnedSize.Div(2)).Sub(position);
            var angle = dif.Atan() + (dif.X >= 0 ? 180 : 0);
            obj.Rotation = angle;
        }
    }
}