using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Painting.Types.Paint;
using Painting.Util;
using Tanks.Backend;
using Tanks.Enums;
using Tanks.Objects.Animation;

namespace Tanks.Objects.GameObjects
{
    public class Field : GameObject
    {
        private ObservableCollection<GameObject> _objects;

        public Field(Coordinate position, Coordinate size, ObservableCollection<GameObject> objects, decimal id)
            : base(
                position, size, 0, new ShapeCollection(new ObservableCollection<Shape>(objects.Select(o => o.View))), id
            )
        {
            Objects = objects;
        }

        public ObservableCollection<GameObject> Objects
        {
            get { return _objects; }
            set
            {
                _objects = value;
                View =
                    new ShapeCollection(new ObservableCollection<Shape>(Objects.Select(o => o.View)));
            }
        }

        public MainPlayer GetMainPlayer => Objects.FirstOrDefault(o => o is MainPlayer) as MainPlayer;

        public void AddObject(AddableObjects obj, InGameEngine engine, Coordinate position = null)
        {
            switch (obj)
            {
                case AddableObjects.MainPlayer:
                    Objects.Add(new MainPlayer(new Coordinate(100, 100), new Coordinate(100, 100), 0, 3,
                        engine.CurrentId, new Coordinate(100,100)));
                    break;
                case AddableObjects.NormalBullet:
                    var player = GetMainPlayer;
                    if (player == null)
                        throw new Exception("No Player defined!");
                    var bullet =
                        new NormalBullet(
                            player.Position.Sub(new Coordinate(0, 10))
                                .Add(player.Size.Div(2))
                                .Add(
                                    new Coordinate(
                                        (float) Math.Cos(Physomatik.ToRadian(player.Rotation))*(player.Size.Pyth/2 + 20),
                                        (float) Math.Sin(Physomatik.ToRadian(player.Rotation))*(player.Size.Pyth/2 + 20))),
                            new Coordinate(10, 30), player.Rotation,
                            engine.CurrentId, 1);
                    Objects.Add(bullet);
                    engine.Animations.Add(new AngularMoveAnimation(bullet, bullet.Rotation, 10));
                    break;
                case AddableObjects.NormalBlock:
                    if (position == null)
                        throw new Exception("Position not specified!");
                    Objects.Add(new Block(position, new Coordinate(100, 100), engine.CurrentId, false));
                    break;
                case AddableObjects.Hole:
                    if (position == null)
                        throw new Exception("Position not specified!");
                    Objects.Add(new Hole(position, new Coordinate(100, 100), engine.CurrentId));
                    break;
                case AddableObjects.Explosion:
                    if(position == null)
                        throw new Exception("Position not specified!");
                    Objects.Add(new Explosion(position, new Coordinate(1,1), engine.CurrentId, new Colour(Color.Black)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
            Objects = Objects;
        }
    }
}