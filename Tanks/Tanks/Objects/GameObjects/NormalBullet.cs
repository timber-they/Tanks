using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class NormalBullet : Bullet
    {
        public NormalBullet(Coordinate position, Coordinate size, float rotation, decimal id,
            int availableCollisionsCount)
            : base(position, size, new Colour(Color.Red), rotation, id, availableCollisionsCount)
        {
        }
    }
}