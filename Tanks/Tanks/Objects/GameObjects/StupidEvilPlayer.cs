using System;
using System.Drawing;
using System.Linq;
using Painting.Types.Paint;
using Tanks.Backend;
using Tanks.Enums;

namespace Tanks.Objects.GameObjects
{
    public class StupidEvilPlayer : Player
    {
        public StupidEvilPlayer(Coordinate position, Coordinate unturnedSiz, float rotation, decimal id,
            Coordinate startPosition, int lives = 1) : base(rotation, lives, position, unturnedSiz, new Colour(Color.Red), id, startPosition, (decimal)1E7)
        {
        }

        public void DoSomething(InGameEngine engine)
        {
            Tracer.TracePosition(engine.Player.CenterPosition(), this);
            if (engine.Field.Objects
                    .Where(o => o is Block)
                    .Any(block => Arithmetic.Cuts(CenterPosition(), block.Position, block.UnturnedSize, Rotation, PublicStuff.NormalBulletSize, 5)))
                return;
            if (DateTime.Now.Ticks - LastShootFired <= ShootTimeLag)
                return;
            engine.Field.AddObject(AddableObjects.NormalBullet, engine, null, this);
            LastShootFired = DateTime.Now.Ticks;
        }
    }
}