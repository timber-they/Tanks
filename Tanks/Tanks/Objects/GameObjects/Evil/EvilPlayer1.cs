using System;
using System.Collections.Generic;
using Painting.Types.Paint;
using Tanks.Backend;
using Tanks.Enums;

namespace Tanks.Objects.GameObjects.Evil
{
    public class EvilPlayer1 : EvilPlayer
    {
        public EvilPlayer1(Coordinate position, Coordinate size, float rotation,
            Coordinate startPosition, InGameEngine engine, int lives) : base(position, size, rotation, startPosition, 1, engine, lives)
        {
            _t = DateTime.Now.Ticks;
        }

        public override void DoSomething() //TODO
        {
            Trace(Engine.MainPlayer.CenterPosition());
            IntelliShoot(Engine.MainPlayer.CenterPosition());
            if (Moves.Count <= 0)
                Move(Direction.Down);
            if (DateTime.Now.Ticks - _t <= (decimal)1E7) return;
            _t = DateTime.Now.Ticks;
            var col = new List<Direction>(Moves);
            foreach (var move in col)
            {
                StopMoving(move);
                Move(DirectionFunctionality.Inverted(move));
            }
        }

        private decimal _t;
    }
}