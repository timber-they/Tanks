using Painting.Types.Paint;
using Tanks.Backend;

namespace Tanks.Objects.GameObjects.Evil
{
    public class EvilPlayer0 : EvilPlayer
    {
        public EvilPlayer0(Coordinate position, Coordinate size, float rotation,
            Coordinate startPosition, InGameEngine engine, int lives) : base(position, size, rotation, startPosition, 0, engine, lives)
        {

        }

        public override void DoSomething()
        {
            IntelliTraceShoot(Engine.MainPlayer.CenterPosition());
        }
    }
}