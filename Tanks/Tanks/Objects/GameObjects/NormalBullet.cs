using System.Drawing;
using Painting.Types.Paint;

namespace Tanks.Objects.GameObjects
{
    public class NormalBullet : Bullet
    {
        public NormalBullet(Coordinate position, Coordinate unturnedSiz, float rotation, decimal id,
            int availableCollisionsCount)
            : base(position, unturnedSiz, new Colour(Color.Red), rotation, id, availableCollisionsCount)
        {
        }
    }
}