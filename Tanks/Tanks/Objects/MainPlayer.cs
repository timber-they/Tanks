using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects
{
    public class MainPlayer : Player
    {
        public MainPlayer(Coordinate position, Coordinate size, float rotation, int lives)
            : base(rotation, lives, position, size, new Colour(Color.GreenYellow))
        {
            
        }
    }
}