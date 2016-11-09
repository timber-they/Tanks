using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Painting.Types.Paint;
using Tanks.Backend;
using Tanks.Enums;
using Tanks.Objects.Animation;
using Rectangle = Painting.Types.Paint.Rectangle;

namespace Tanks.Objects.GameObjects
{
    public class Player : GameObject
    {
        protected Player(float rotation, int lives, Coordinate position, Coordinate unturnedSize, Colour colour, Coordinate startPosition, decimal shootTimeLag, InGameEngine engine)
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
                    {Position = new Coordinate(0, 0)}, engine.CurrentId)
        {
            ShootTimeLag = shootTimeLag;
            StartPosition = startPosition;
            Lives = lives;
            Engine = engine;
            View.Shapes[0].MainColour = new Colour(Color.FromArgb(colour.Color.ToArgb()*3));
            Moves = new ObservableCollection<Direction>();
            var polygon = View.Shapes[1] as Rectangle;
            if (polygon != null)
                polygon.LineColour = colour;
        }

        public int Lives { get; set; }
        private Coordinate StartPosition { get; set; }
        private decimal LastShootFired { get; set; }
        private decimal ShootTimeLag { get; }
        protected InGameEngine Engine { get; }

        public void Move(Direction direction)
        {
            if (DirectionValid(direction) && !Engine.Animations.Any(
                    animation =>
                        (animation.AnimatedObject.Id == Id) &&
                        (((NormalMoveAnimation)animation).Direction == direction)))
                Engine.Animations =
                    new ObservableCollection<Animation.Animation>(Engine.Animations.Where(
                            animation =>
                                !((animation.AnimatedObject.Id == Id) &&
                                  (((NormalMoveAnimation)animation).Direction ==
                                   DirectionFunctionality.Inverted(direction))))
                        .ToList()) { new NormalMoveAnimation(this, direction, 3) };
            {
                Moves.Add(direction);
            }
        }

        private bool DirectionValid(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return Position.X < Engine.Field.UnturnedSize.X &&
                           Position.X + UnturnedSize.X < Engine.Field.UnturnedSize.X;
                case Direction.Down:
                    return Position.Y < Engine.Field.UnturnedSize.Y &&
                           Position.Y + UnturnedSize.Y < Engine.Field.UnturnedSize.Y;
                case Direction.Left:
                    return Position.X > 0 &&
                           Position.X + UnturnedSize.X > 0;
                case Direction.Up:
                    return Position.Y > 0 &&
                           Position.Y + UnturnedSize.Y > 0;
                case Direction.Nothing:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public void StopMoving(Direction direction)
        {
            Engine.Animations = new ObservableCollection<Animation.Animation>(
                Engine.Animations.Where(
                        animation =>
                            !((animation.AnimatedObject.Id == Id) &&
                              (((NormalMoveAnimation)animation).Direction == direction)))
                    .ToList());
            Moves.Remove(direction);
        }

        protected ObservableCollection<Direction> Moves { get; set; }

        public void Shoot()
        {
            if (DateTime.Now.Ticks - LastShootFired <= ShootTimeLag)
                return;
            Engine.Field.AddObject(AddableObjects.NormalBullet, Engine, null, 0, this);
            LastShootFired = DateTime.Now.Ticks;
        }

        public void GoToStartPosition() => Position = StartPosition;

        /// <summary>
        /// The animation still must be changed after calling this method!
        /// </summary>
        public void Die()
        {
            if (Lives < 2)
            {
                if (this is MainPlayer)
                    Engine.Window.Close();
                else
                    Engine.Field.Objects.Remove(this);
            }
            Engine.Field.AddObject(AddableObjects.NotDestroyingExplosion, Engine, CenterPosition());
            GoToStartPosition();
            Lives -= 1;
        }
    }
}