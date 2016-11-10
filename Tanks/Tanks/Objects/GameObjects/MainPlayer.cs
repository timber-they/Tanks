using System.Drawing;
using Painting.Types.Paint;
using Tanks.Backend;

namespace Tanks.Objects.GameObjects
{
    public class MainPlayer : Player
    {
        public MainPlayer(Coordinate position, Coordinate size, float rotation, int lives, Coordinate startPosition, InGameEngine engine)
            : base(rotation, lives, position, size, new Colour(Color.GreenYellow), startPosition, (decimal)5E6, engine, typeof(NormalBullet))
        {
        }
    }
}