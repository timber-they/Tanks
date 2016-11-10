using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Painting.Types.Paint;
using Tanks.Backend;
using Tanks.Enums;

namespace Tanks.Objects.GameObjects.Evil
{
    public class EvilPlayer : Player
    {
        public EvilPlayer(Coordinate position, Coordinate size, float rotation,
            Coordinate startPosition, int intelligenceLevel, InGameEngine engine, int lives = 1)
            : base(rotation, lives, position, size, new Colour(Color.Red), startPosition, (decimal)1E7, engine, typeof(NormalBullet))
        {
            IntelligenceLevel = intelligenceLevel;
        }

        public virtual void DoSomething()
        {
        }

        protected void IntelliTrace(Coordinate aim) //TODO In jede Himmelsrichtung verfolgen (Abprallpunkt bestimmen, Schauen, ob auf dem Hinweg/Rückweg ein Zusammenstoß passiert, bevor es den Gegner trifft(area)
        {
            var bad = true;
            Direction direction = Direction.Up;
            do
            {
                
            } while (bad);
        }

        protected void Trace(Coordinate aim) => Tracer.TracePosition(aim, this);
        protected void IntelliShoot(Coordinate aim, int intelliState = 0)
        {
            if (IntelliCutsAnything(new Area(aim, Position), intelliState))
                return;
            Shoot();
        }

        /// <summary>
        /// Doesn't calculate rebounding
        /// </summary>
        /// <param name="area"></param>
        /// <param name="intelliState"></param>
        /// <returns></returns>
        private bool IntelliCutsAnything(Area area, int intelliState=0) => Engine.Field.Objects
            .Where(o => (o is Block || intelliState > 0 && o is EvilPlayer) && (area.IsCoordinateInArea(o.Position) || area.IsCoordinateInArea(o.Position.Add(o.Size))))
            .Any(o =>
                Arithmetic.Cuts(CenterPosition(), o.Position, o.Size, Rotation,
                    PublicStuff.NormalBulletSize, 5));

        protected void IntelliTraceShoot(Coordinate aim, int intelliState = 0)
        {
            switch (intelliState)
            {
                case 0:
                    Trace(aim);
                    IntelliShoot(aim, intelliState);
                    break;
                case 1:
                    Trace(aim);
                    if (IntelliCutsAnything(new Area(aim, Position), intelliState) && BulletType == typeof(NormalBullet))
                    {

                    }
                    break;
            }
        }

        private int IntelligenceLevel { get; }
    }
}