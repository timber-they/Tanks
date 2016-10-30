using System;
using System.Collections.ObjectModel;
using System.Linq;
using Tanks.Enums;
using Tanks.Objects;
using Tanks.Objects.Animation;

namespace Tanks.Backend
{
    public class Collision
    {
        public static ObservableCollection<Animation> UpdateAnimations (ObservableCollection<Animation> animations, InGameEngine engine)
        {
            var fin = new ObservableCollection<Animation> ();
            animations.Where (
                animation => ObjectColliding (animation.AnimatedObject, engine, animation) == Colliding.NoCollide);
            foreach (var animation in animations)
            {
                var colliding = ObjectColliding (animation.AnimatedObject, engine, animation);
                switch (colliding)
                {
                    case Colliding.NoCollide:
                        fin.Add (animation);
                        break;
                    case Colliding.Collided:
                        break;
                    case Colliding.ReboundedHorizontal:
                    case Colliding.ReboundedVertical:
                        var gameObject = engine.Field.Objects.FirstOrDefault (o => o.Id == animation.AnimatedObject.Id);
                        if (gameObject == null)
                            break;
                        gameObject.Rotation = (colliding == Colliding.ReboundedHorizontal ? 180 - gameObject.Rotation :  -gameObject.Rotation) % 360;
                        ((Bullet) gameObject).AvailableCollisionCount--;
                        fin.Add (new AngularMoveAnimation (gameObject, gameObject.Rotation, engine, 10));
                        break;
                    case Colliding.Nothing:
                        throw new NotImplementedException ("Colliding for this Object not yet implemented!");
                    default:
                        throw new ArgumentOutOfRangeException ();
                }
            }
            return fin;
        }

        public static void UpdateObjects ()
        {

        }

        public static Colliding ObjectColliding (GameObject obj, InGameEngine engine, Animation animation)
        {
            var bullet = obj as Bullet;
            var player = obj as Player;
            if (bullet != null)
                return BulletColliding (bullet, engine);
            if (player != null)
                return PlayerColliding (player, engine, ((NormalMoveAnimation) animation).Direction);
            return Colliding.Collided;

        }

        public static Colliding BulletColliding (Bullet bullet, InGameEngine engine)
        {
            var normalBullet = bullet as NormalBullet;
            if (normalBullet != null)
            {
                var xinvalid = BiggerThanFieldX(normalBullet, engine.Field) || normalBullet.Position.X < 0;
                var yinvalid = BiggerThanFieldY(normalBullet, engine.Field) || normalBullet.Position.Y < 0;
                return xinvalid || yinvalid
                    ? (normalBullet.AvailableCollisionCount > 0 ? (xinvalid ? Colliding.ReboundedHorizontal : Colliding.ReboundedVertical) : Colliding.Collided)
                    : Colliding.NoCollide;
            }
            return Colliding.Nothing;
        }

        public static Colliding PlayerColliding (Player player, InGameEngine engine, Direction direction)
        {
            var mainPlayer = player as MainPlayer;
            if (mainPlayer != null)
                return BiggerThanFieldX (mainPlayer, engine.Field) &&
                       direction == Direction.Right ||
                       BiggerThanFieldY (mainPlayer, engine.Field) &&
                       direction == Direction.Down ||
                       mainPlayer.Position.X < 0 &&
                       direction == Direction.Left ||
                       mainPlayer.Position.Y < 0 &&
                       direction == Direction.Up ? Colliding.Collided : Colliding.NoCollide;
            return Colliding.Nothing;
        }

        private static bool BiggerThanFieldX (GameObject obj, GameObject field)
            => obj.Position.X > field.Size.X || obj.Position.X + obj.Size.X > field.Size.X;

        private static bool BiggerThanFieldY (GameObject obj, GameObject field)
            => obj.Position.Y > field.Size.Y || obj.Position.Y + obj.Size.Y > field.Size.Y;
    }
}