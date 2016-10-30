using System;
using System.Collections.ObjectModel;
using System.Linq;
using Painting.Types.Paint;
using Painting.Util;
using Tanks.Backend;
using Tanks.Enums;
using Tanks.Objects.Animation;

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

        public Field (Coordinate position, Coordinate size, ObservableCollection<GameObject> objects, decimal id)
            : base (position, size, 0, new ShapeCollection (new ObservableCollection<Shape> (objects.Select (o => o.View))), id)
        {
            Objects = objects;
        }

        public void AddObject(AddableObjects obj, InGameEngine engine)
        {
            switch (obj)
            {
                case AddableObjects.MainPlayer:
                    Objects.Add (new MainPlayer (new Coordinate (100, 100), new Coordinate (100, 100), 0, 3, engine.CurrentId));
                    break;
                case AddableObjects.NormalBullet:
                    var player = GetMainPlayer;
                    if(player == null)
                        throw new Exception("No Player defined!");
                    var bullet = new NormalBullet(player.Position.Add(player.Size.Div(2)).Add(new Coordinate((float)Math.Cos(Physomatik.ToRadian(player.Rotation)) * player.Size.Pyth / 2, (float)Math.Sin(Physomatik.ToRadian(player.Rotation)) * player.Size.Pyth / 2)), new Coordinate(10, 30), player.Rotation,
                        engine.CurrentId, 1);
                    Objects.Add(bullet);
                    engine.Animations.Add(new AngularMoveAnimation(bullet, bullet.Rotation, engine, 10));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
            Objects = Objects;
        }

        public MainPlayer GetMainPlayer => Objects.FirstOrDefault(o => o is MainPlayer) as MainPlayer;
    }
}