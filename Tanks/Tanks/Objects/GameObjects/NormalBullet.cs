using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class NormalBullet : Bullet
    {
        public NormalBullet(Coordinate position, Coordinate unturnedSize, float rotation, decimal id,
            int availableCollisionsCount)
            : base(position, unturnedSize, new Colour(Color.Red), rotation, id, availableCollisionsCount)
        {
        }
    }
}