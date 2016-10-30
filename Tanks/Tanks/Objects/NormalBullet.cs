using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects
{
    public class NormalBullet : Bullet
    {
        public NormalBullet(Coordinate position, Coordinate size, float rotation, decimal id, int availableCollisiontCount)
            : base(position, size, new Colour(Color.Red), rotation, id, availableCollisiontCount)
        {
            
        }
    }
}