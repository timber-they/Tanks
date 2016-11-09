using Painting.Types.Paint;
using Tanks.Backend;

namespace Tanks.Objects.GameObjects
{
    public class EvilPlayer1 : EvilPlayer
    {
        public EvilPlayer1(Coordinate position, Coordinate unturnedSize, float rotation, decimal id,
            Coordinate startPosition, InGameEngine engine, int lives) : base(position, unturnedSize, rotation, id, startPosition, 1, engine, lives)
        {

        }

        public override void DoSomething()
        {

        }
    }
}