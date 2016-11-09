using System.Drawing;
using Painting.Types.Paint;
using Tanks.Backend;

namespace Tanks.Objects.GameObjects
{
    public class MainPlayer : Player
    {
        public MainPlayer(Coordinate position, Coordinate unturnedSize, float rotation, int lives, Coordinate startPosition, InGameEngine engine)
            : base(rotation, lives, position, unturnedSize, new Colour(Color.GreenYellow), startPosition, (decimal)5E6, engine)
        {
        }
    }
}