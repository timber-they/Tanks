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
        }

        public override void DoSomething() //TODO
        {
            IntelliTraceShoot(Engine.MainPlayer.CenterPosition(), 1);
        }
    }
}