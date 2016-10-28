using System.Collections.ObjectModel;
using System.Linq;
using Painting.Types.Paint;

namespace Tanks.Objects
{
    public class Field : GameObject
    {
        private ObservableCollection<GameObject> _objects;

        public ObservableCollection<GameObject> Objects
        {
            get { return _objects; }
            set
            {
                _objects = value;
                View =
                    new ShapeCollection (new ObservableCollection<Shape> (Objects.Select (o => o.View)))
                    {
                            /*TODO:The view of the field itself*/
                    };
            }
        }

        public Field (Coordinate position, Coordinate size, ObservableCollection<GameObject> objects)
            : base (position, size, 0, new ShapeCollection (new ObservableCollection<Shape> (objects.Select (o => o.View)) {/*TODO:The view of the field itself*/}))
        {
            Objects = objects;
        }
    }
}