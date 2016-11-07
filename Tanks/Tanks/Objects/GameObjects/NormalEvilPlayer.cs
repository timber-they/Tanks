using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class NormalEvilPlayer : Player
    {
        public NormalEvilPlayer(Coordinate position, Coordinate size, float rotation, decimal id,
            Coordinate startPosition, int lives = 1) : base(rotation, lives, position, size, new Colour(Color.Red), id, startPosition)
        {
        }
    }
}