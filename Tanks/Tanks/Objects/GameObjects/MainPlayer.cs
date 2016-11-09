using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class MainPlayer : Player
    {
        public MainPlayer(Coordinate position, Coordinate unturnedSize, float rotation, int lives, decimal id, Coordinate startPosition)
            : base(rotation, lives, position, unturnedSize, new Colour(Color.GreenYellow), id, startPosition, (decimal)5E6)
        {
        }
    }
}