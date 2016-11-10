using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Painting.Types.Paint;
using Painting.Util;
using Tanks.Backend;
using Tanks.Enums;
using Tanks.Objects.Animation;
using Tanks.Objects.GameObjects.Evil;

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

        public void AddObject(AddableObjects obj, InGameEngine engine, Coordinate position = null, int evilPlayerLevel = 0, Player player = null)
        {
            if (AddableObjectsFunctionality.PositionRequired(obj) && position == null)
                throw new Exception("Position not specified!");
            switch (obj)
            {
                case AddableObjects.MainPlayer:
                    Objects.Add(new MainPlayer(new Coordinate(100, 100), new Coordinate(100, 100), 0, 3, new Coordinate(100, 100), engine));
                    break;
                case AddableObjects.EvilPlayer:
                    switch (evilPlayerLevel)
                    {
                        case 0:
                            Objects.Add(new EvilPlayer0(position, new Coordinate(100, 100), 0, position, engine, engine.Debugging ? 100 : 1));
                            break;
                        case 1:
                            Objects.Add(new EvilPlayer1(position, new Coordinate(100, 100), 0, position, engine, engine.Debugging ? 100 : 1));
                            break;
                    }
                    break;
                case AddableObjects.NormalBullet:
                    player = player ?? GetMainPlayer;
                    if (player == null)
                        throw new Exception("No Player defined!");
                    var bullet =
                        new NormalBullet(
                            player.Position.Sub(new Coordinate(0, 10))
                                .Add(player.Size.Div(2))
                                .Add(
                                    new Coordinate(
                                        (float)Math.Cos(Physomatik.ToRadian(player.Rotation)) * (player.Size.Pyth() / 2 + 20),
                                        (float)Math.Sin(Physomatik.ToRadian(player.Rotation)) * (player.Size.Pyth() / 2 + 20))),
                            PublicStuff.NormalBulletSize, player.Rotation,
                            engine.CurrentId);
                    Objects.Add(bullet);
                    engine.Animations.Add(new AngularMoveAnimation(bullet, bullet.Rotation, 10));
                    break;
                case AddableObjects.NormalBlock:
                    Objects.Add(new Block(position, new Coordinate(100, 100), engine.CurrentId, false, new Colour(Color.FromArgb(-4144960))));
                    break;
                case AddableObjects.DestroyableBlock:
                    Objects.Add(new Block(position, new Coordinate(100, 100), engine.CurrentId, true, new Colour(Color.FromArgb(173, 102, 31))));
                    break;
                case AddableObjects.Hole:
                    Objects.Add(new Hole(position, new Coordinate(100, 100), engine.CurrentId));
                    break;
                case AddableObjects.NotDestroyingExplosion:
                    Objects.Add(new Explosion(position, new Coordinate(1, 1), engine.CurrentId, new Colour(Color.Black), new Colour(Color.Red), false, 5));
                    break;
                case AddableObjects.DestroyingExplosion:
                    Objects.Add(new Explosion(position, new Coordinate(1, 1), engine.CurrentId, new Colour(Color.Black), new Colour(Color.OrangeRed), true, 5));
                    break;
                case AddableObjects.Mine:
                    Objects.Add(new Mine(position, new Coordinate(30, 30), engine.CurrentId, 5, new Colour(Color.FromArgb(-256)), new Colour(Color.FromArgb(150, 255, 50)), new Coordinate(150, 150)));
                    engine.Animations.Add(new MineAnimation(Objects.Last(), 2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
            Objects = Objects;
        }

        public void Paint(Graphics p)
        {
            var trans = p.Transform.Clone();
            var rotationCenterPointFromPosition = CenterPosition();
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (Rotation != 0)
            {
                p.TranslateTransform(rotationCenterPointFromPosition.X, rotationCenterPointFromPosition.Y);
                p.RotateTransform(Rotation);
                p.TranslateTransform(-rotationCenterPointFromPosition.X, -rotationCenterPointFromPosition.Y);
            }
            var bullets = Objects.Where(o => o is Bullet).ToList();
            var players = Objects.Where(o => o is Player).ToList();
            var others = Objects.Where(o => bullets.All(b => b.Id != o.Id) && players.All(l => l.Id != o.Id)).ToList();
            foreach (var other in others)
                other.View.Paint(p, other.CenterPosition());
            foreach (var player in players)
                player.View.Paint(p, player.CenterPosition());
            foreach (var bullet in bullets)
                bullet.View.Paint(p, bullet.CenterPosition());
            p.Transform = trans;
        }
    }
}