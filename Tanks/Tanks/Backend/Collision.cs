using System;
using System.Collections.ObjectModel;
using System.Linq;
using Painting.Types.Paint;
using Tanks.Enums;
using Tanks.Objects;
using Tanks.Objects.Animation;
using Tanks.Objects.GameObjects;

namespace Tanks.Backend
{
    public class Collision
    {
        public static ObservableCollection<Animation> UpdateAnimations (ObservableCollection<Animation> animations,
            InGameEngine engine)
        {
            var fin = new ObservableCollection<Animation> ();
            foreach (var animation in animations)
            {
                var colliding = ObjectColliding (animation.AnimatedObject, engine, animation);
                var gameObject = engine.Field.Objects.FirstOrDefault (o => o.Id == animation.AnimatedObject.Id);
                switch (colliding)
                {
                    case Colliding.NoCollide:
                        fin.Add (animation);
                        break;
                    case Colliding.Collided:
                        break;
                    case Colliding.ReboundedHorizontal:
                    case Colliding.ReboundedVertical:
                        if (gameObject == null)
                            break;
                        gameObject.Rotation = (colliding == Colliding.ReboundedHorizontal
                                                  ? 180 - gameObject.Rotation
                                                  : -gameObject.Rotation) % 360;
                        ((Bullet) gameObject).AvailableCollisionCount--;
                        fin.Add (new AngularMoveAnimation (gameObject, gameObject.Rotation, engine, 10));
                        break;
                    case Colliding.PlayerVanishing:
                        if(engine.Player.Lives < 1)
                            engine.Window.Close();
                        engine.Player.Position = new Coordinate (100, 100);
                        engine.Player.Lives -= 1;
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
                return PlayerColliding (player, engine, ((NormalMoveAnimation) animation).Direction,
                    (NormalMoveAnimation) animation);
            return Colliding.Nothing;
        }

        public static Colliding BulletColliding (Bullet bullet, InGameEngine engine)
        {
            foreach (var o in engine.Field.Objects.Where (o => o is Block))
            {
                var cutting = bullet.Cuts (o);
                if (cutting == Direction.Nothing)
                    continue;
                return bullet.AvailableCollisionCount > 0
                    ? (cutting == Direction.Left || cutting == Direction.Right
                        ? Colliding.ReboundedHorizontal
                        : Colliding.ReboundedVertical)
                    : Colliding.Collided;
            }
            if (engine.Field.Objects.Where (o => o is Bullet)
                .Select (engine.Player.Cuts)
                .Any (cutting => cutting != Direction.Nothing))
                return Colliding.PlayerVanishing;
            var normalBullet = bullet as NormalBullet;
            if (normalBullet == null)
                return Colliding.Nothing;
            var xinvalid = BiggerThanFieldX (normalBullet, engine.Field) || normalBullet.Position.X < 0;
            var yinvalid = BiggerThanFieldY (normalBullet, engine.Field) || normalBullet.Position.Y < 0;
            return xinvalid || yinvalid
                ? (normalBullet.AvailableCollisionCount > 0
                    ? (xinvalid ? Colliding.ReboundedHorizontal : Colliding.ReboundedVertical)
                    : Colliding.Collided)
                : Colliding.NoCollide;
        }

        public static Colliding PlayerColliding (Player player, InGameEngine engine, Direction direction,
            NormalMoveAnimation animation)
        {
            if (engine.Field.Objects.Where (o => o is Block)
                    .Select (player.Cuts)
                    .Any (cutting => cutting != Direction.Nothing && animation.Direction == cutting))
                return Colliding.Collided;
            if (engine.Field.Objects.Where (o => o is Hole || o is Bullet)
                .Select (player.Cuts)
                .Any (cutting => cutting != Direction.Nothing))
                return Colliding.PlayerVanishing;

            var mainPlayer = player as MainPlayer;
            if (mainPlayer != null)
                return BiggerThanFieldX (mainPlayer, engine.Field) &&
                       direction == Direction.Right ||
                       BiggerThanFieldY (mainPlayer, engine.Field) &&
                       direction == Direction.Down ||
                       mainPlayer.Position.X < 0 &&
                       direction == Direction.Left ||
                       mainPlayer.Position.Y < 0 &&
                       direction == Direction.Up
                    ? Colliding.Collided
                    : Colliding.NoCollide;
            return Colliding.Nothing;
        }

        private static bool BiggerThanFieldX (GameObject obj, GameObject field)
            => obj.Position.X > field.Size.X || obj.Position.X + obj.Size.X > field.Size.X;

        private static bool BiggerThanFieldY (GameObject obj, GameObject field)
            => obj.Position.Y > field.Size.Y || obj.Position.Y + obj.Size.Y > field.Size.Y;
    }
}