using System;
using Painting.Types.Paint;
using Painting.Util;

namespace Tanks.Backend
{
    public static class Arithmetic
    {
        public static bool Cuts(Coordinate startPoint, Coordinate objPosition, Coordinate objSize, float angle, Coordinate flyingObjectSize, float tolerance=0) //TODO fix
        {
            if (Math.Abs(objSize.Min()) < 0.001)
                return false;
            var m = (float)Math.Tan(Physomatik.ToRadian(angle));
            var step = objPosition.X > startPoint.X ? 1 : -1;
            for (var tx = startPoint.X; (objPosition.X > startPoint.X ? tx < objPosition.X : tx > objPosition.X); tx+=step)
            {
                var ty = m*(tx - startPoint.X) + startPoint.Y;
                if ((tx + flyingObjectSize.X / 2 + tolerance >= objPosition.X || tx - flyingObjectSize.X / 2 + tolerance >= objPosition.X) &&
                    (tx + flyingObjectSize.X / 2 - tolerance <= objPosition.X + objSize.X || tx - flyingObjectSize.X / 2 - tolerance <= objPosition.X + objSize.X) && 
                    (ty + flyingObjectSize.Y / 2 + tolerance >= objPosition.Y || ty - flyingObjectSize.Y / 2 + tolerance >= objPosition.Y) &&
                    (ty + flyingObjectSize.Y / 2 - tolerance <= objPosition.Y + objSize.Y || ty - flyingObjectSize.Y / 2 - tolerance <= objPosition.Y + objSize.Y))
                    return true;
            }
            return false;
        }

        public static float RealAngle(float angle)
        {
            var fin = angle;
            while (fin < 0)
                fin += 360;;
            return fin % 360;
        }
    }
}