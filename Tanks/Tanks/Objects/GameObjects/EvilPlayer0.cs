using System;
using System.Linq;
using Painting.Types.Paint;
using Tanks.Backend;
using Tanks.Enums;

namespace Tanks.Objects.GameObjects
{
    public class EvilPlayer0 : EvilPlayer
    {
        public EvilPlayer0(Coordinate position, Coordinate unturnedSize, float rotation, decimal id,
            Coordinate startPosition, InGameEngine engine, int lives) : base(position, unturnedSize, rotation, id, startPosition, 0, engine, lives)
        {

        }

        public override void DoSomething()
        {
            Trace();
            IntelliShoot();
        }
    }
}