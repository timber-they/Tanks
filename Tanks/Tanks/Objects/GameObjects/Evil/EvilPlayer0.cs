using Painting.Types.Paint;
using Tanks.Backend;

namespace Tanks.Objects.GameObjects.Evil
{
    public class EvilPlayer0 : EvilPlayer
    {
        public EvilPlayer0(Coordinate position, Coordinate unturnedSize, float rotation,
            Coordinate startPosition, InGameEngine engine, int lives) : base(position, unturnedSize, rotation, startPosition, 0, engine, lives)
        {

        }

        public override void DoSomething()
        {
            Trace();
            IntelliShoot();
        }
    }
}