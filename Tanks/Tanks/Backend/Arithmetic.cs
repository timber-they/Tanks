using System;
using Painting.Types.Paint;
using Painting.Util;

namespace Tanks.Backend
{
    public static class Arithmetic
    {
        public static bool Cuts(Coordinate startPoint, Coordinate objPosition, Coordinate objSize, float angle)
        {
            if (Math.Abs(objSize.Min) < 0.001)
                return false;
            var m = (float)Math.Tan(Physomatik.ToRadian(angle));
            var step = objPosition.X > startPoint.X ? 1 : -1;
            for (var tx = startPoint.X; (objPosition.X > startPoint.X ? tx < objPosition.X : tx > objPosition.X); tx+=step)
            {
                var ty = m*(tx - startPoint.X) + startPoint.Y;
                if (tx >= objPosition.X && 
                    tx <= objPosition.X + objSize.X && 
                    ty >= objPosition.Y &&
                    ty <= objPosition.Y + objSize.Y)
                    return true;
            }
            return false;
        }
    }
}