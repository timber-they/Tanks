using System.Drawing;
using System.Linq;
using Painting.Types.Paint;
using Tanks.Backend;

namespace Tanks.Objects.GameObjects.Evil
{
    public class EvilPlayer : Player
    {
        public EvilPlayer(Coordinate position, Coordinate unturnedSize, float rotation,
            Coordinate startPosition, int intelligenceLevel, InGameEngine engine, int lives = 1) 
            : base(rotation, lives, position, unturnedSize, new Colour(Color.Red), startPosition, (decimal)1E7, engine)
        {
            IntelligenceLevel = intelligenceLevel;
        }

        public virtual void DoSomething()
        {
        }

        protected void Trace() => Tracer.TracePosition(Engine.MainPlayer.CenterPosition(), this);
        protected void IntelliShoot()
        {
            if (Engine.Field.Objects
                    .Where(o => o is Block)
                    .Any(block => Arithmetic.Cuts(CenterPosition(), block.Position, block.UnturnedSize, Rotation, PublicStuff.NormalBulletSize, 5)))
                return;
            Shoot();
        }

        public int IntelligenceLevel { get; protected set; }
    }
}