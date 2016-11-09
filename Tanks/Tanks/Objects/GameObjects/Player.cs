using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Painting.Types.Paint;
using Tanks.Backend;
using Tanks.Enums;
using Tanks.Objects.Animation;
using Rectangle = Painting.Types.Paint.Rectangle;

namespace Tanks.Objects.GameObjects
{
    public class Player : GameObject
    {
        protected Player(float rotation, int lives, Coordinate position, Coordinate unturnedSize, Colour colour,
            decimal id, Coordinate startPosition, decimal shootTimeLag)
            : base(
                position, unturnedSize, rotation,
                new ShapeCollection(new ObservableCollection<Shape>
                    {
                        new Line(new Coordinate(57, 57), new Coordinate(149, 57), new Colour(Color.FromArgb(-16711936)),
                            7),
                        new Rectangle(2, new Colour(Color.FromArgb(-65536)), new Coordinate(17, 15),
                            new Coordinate(84, 84),
                            new Colour(Color.Empty), 0),
                        new Ellipse(0, new Colour(Color.Empty), new Coordinate(7, 8), new Coordinate(100, 100),
                            new Colour(Color.FromArgb(-16777216)), 0)
                    })
                    {Position = new Coordinate(0, 0)}, id)
        {
            ShootTimeLag = shootTimeLag;
            StartPosition = startPosition;
            Lives = lives;
            View.Shapes[0].MainColour = new Colour(Color.FromArgb(colour.Color.ToArgb()*3));
            var polygon = View.Shapes[1] as Rectangle;
            if (polygon != null)
                polygon.LineColour = colour;
        }

        public int Lives { get; set; }
        public Coordinate StartPosition { get; private set; }
        public decimal LastShootFired { get; set; }
        public decimal ShootTimeLag { get; private set; }

        public void Move(InGameEngine engine, Direction direction)
        {
            if (Position.X > 10 && Position.X + UnturnedSize.X > 10 && !engine.Animations.Any(
                    animation =>
                        (animation.AnimatedObject.Id == Id) &&
                        (((NormalMoveAnimation) animation).Direction == direction)))
                engine.Animations =
                    new ObservableCollection<Animation.Animation>(engine.Animations.Where(
                            animation =>
                                !((animation.AnimatedObject.Id == Id) &&
                                  (((NormalMoveAnimation) animation).Direction ==
                                   DirectionFunctionality.Inverted(direction))))
                        .ToList()) {new NormalMoveAnimation(this, direction, 3)};
        }

        public void StopMoving(InGameEngine engine, Direction direction)
            => engine.Animations = new ObservableCollection<Animation.Animation>(
                engine.Animations.Where(
                        animation =>
                            !((animation.AnimatedObject.Id == Id) &&
                              (((NormalMoveAnimation) animation).Direction == direction)))
                    .ToList());

        public void Shoot(InGameEngine engine)
        {
            if (DateTime.Now.Ticks - LastShootFired <= ShootTimeLag)
                return;
            engine.Field.AddObject(AddableObjects.NormalBullet, engine, null, 0, this);
            LastShootFired = DateTime.Now.Ticks;
        }
    }
}