using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tanks.Enums;
using Tanks.Objects.Animation;
using Tanks.Objects.GameObjects;

namespace Tanks.Backend
{
    public static class AnimationUpdating
    {
        public static ObservableCollection<Animation> UpdateAnimations(IEnumerable<Animation> animations,
            InGameEngine engine)
        {
            var fin = new ObservableCollection<Animation>();
            var enumerable = animations as IList<Animation> ?? animations.ToList();
            for (var i = 0; i < enumerable.Count; i++)
            {
                var animation = enumerable[i];
                var gameObject = engine.Field.Objects.FirstOrDefault(o => o.Id == animation.AnimatedObject.Id);
                if (animation is AngularMoveAnimation || animation is NormalMoveAnimation)
                {
                    var colliding = ObjectColliding(animation.AnimatedObject, engine, animation);
                    switch (colliding)
                    {
                        case Colliding.NoCollide:
                            fin.Add(animation);
                            break;
                        case Colliding.Collided:
                            if (!(gameObject is Bullet))
                                break;
                            engine.Field.AddObject(AddableObjects.NotDestroyingExplosion, engine,
                                gameObject.CenterPosition);
                            engine.Animations.Add(new ExplodeAnimation(engine.Field.Objects.Last(), 10f,
                                gameObject.Size.QuadraticForm));
                            break;
                        case Colliding.ReboundedHorizontal:
                        case Colliding.ReboundedVertical:
                            if (gameObject == null)
                                break;
                            gameObject.Rotation = (colliding == Colliding.ReboundedHorizontal
                                                      ? 180 - gameObject.Rotation
                                                      : -gameObject.Rotation)%360;
                            ((Bullet) gameObject).AvailableCollisionCount--;
                            fin.Add(new AngularMoveAnimation(gameObject, gameObject.Rotation, 10));
                            break;
                        case Colliding.PlayerVanishing:
                            PlayerVanishing(engine);
                            break;
                        case Colliding.MineShooted:
                            ((Mine) engine.Field.Objects
                                .Where(o => o is Mine).First(m => engine.Field.Objects
                                    .Any(b => b is Bullet && b.Cuts(m) != Direction.Nothing))).Timer = 0;
                            break;
                        case Colliding.Nothing:
                            throw new NotImplementedException("Colliding for this Object not yet implemented!");
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (animation is ExplodeAnimation && gameObject != null &&
                         ((ExplodeAnimation)animation).MaxSize.CompareTo(gameObject.Size) == 1)
                {
                    fin.Add(animation);
                    if (!((Explosion)animation.AnimatedObject).Destroying) continue;
                    for (var j = 0; j < engine.Field.Objects.Count; j++)
                    {
                        var obj = engine.Field.Objects[j];
                        var block = obj as Block;
                        var player = obj as Player;
                        if (block != null && block.Destroyable &&
                            animation.AnimatedObject.Cuts(block) != Direction.Nothing)
                        {
                            engine.Field.Objects.Remove(block);
                            engine.Field.AddObject(AddableObjects.NotDestroyingExplosion, engine, block.CenterPosition);
                            engine.Animations.Add(new ExplodeAnimation(engine.Field.Objects.Last(), 5, block.Size));
                        }
                        if (player != null && animation.AnimatedObject.Cuts(player) != Direction.Nothing)
                            PlayerVanishing(engine);
                    }
                }
                else if (animation is MineAnimation && gameObject != null)
                {
                    if ((gameObject as Mine)?.Timer > 0)
                        fin.Add(animation);
                    else
                    {
                        engine.Field.AddObject(AddableObjects.DestroyingExplosion, engine,
                            animation.AnimatedObject.CenterPosition);
                        fin.Add(new ExplodeAnimation(engine.Field.Objects.Last(), 10,
                            ((Mine)animation.AnimatedObject).ExplosionSize));
                    }
                }
            }
            return fin;
        }

        private static void PlayerVanishing(InGameEngine engine)
        {
            if (engine.Player.Lives < 2)
                engine.Window.Close();
            engine.Field.AddObject(AddableObjects.NotDestroyingExplosion, engine, engine.Player.CenterPosition);
            engine.Animations.Add(new ExplodeAnimation(engine.Field.Objects.Last(), 10,
                engine.Player.Size));
            engine.Player.Position = engine.Player.StartPosition;
            engine.Player.Lives -= 1;
        }

        private static Colliding ObjectColliding(GameObject obj, InGameEngine engine, Animation animation)
        {
            var bullet = obj as Bullet;
            var player = obj as Player;
            if (bullet != null)
                return BulletColliding(bullet, engine);
            if (player != null)
                return PlayerColliding(player, engine, ((NormalMoveAnimation)animation).Direction,
                    (NormalMoveAnimation)animation);
            return Colliding.Nothing;
        }

        private static Colliding BulletColliding(Bullet bullet, InGameEngine engine)
        {
            foreach (var o in engine.Field.Objects.Where(o => o is Block))
            {
                var cutting = bullet.Cuts(o);
                if (cutting == Direction.Nothing)
                    continue;
                return bullet.AvailableCollisionCount > 0
                    ? ((cutting == Direction.Left) || (cutting == Direction.Right)
                        ? Colliding.ReboundedHorizontal
                        : Colliding.ReboundedVertical)
                    : Colliding.Collided;
            }
            if (engine.Field.Objects.Where(o => o is Bullet)
                .Select(engine.Player.Cuts)
                .Any(cutting => cutting != Direction.Nothing))
                return Colliding.PlayerVanishing;
            if (engine.Field.Objects
                .Where(o => o is Bullet)
                .Any(b => engine.Field.Objects
                .Any(o => o is Mine && o.Cuts(b) != Direction.Nothing)))
                return Colliding.MineShooted;
            var normalBullet = bullet as NormalBullet;
            if (normalBullet == null)
                return Colliding.Nothing;
            var xInvalid = BiggerThanFieldX(normalBullet, engine.Field) || (normalBullet.Position.X < 0);
            var yinvalid = BiggerThanFieldY(normalBullet, engine.Field) || (normalBullet.Position.Y < 0);
            return xInvalid || yinvalid
                ? (normalBullet.AvailableCollisionCount > 0
                    ? (xInvalid ? Colliding.ReboundedHorizontal : Colliding.ReboundedVertical)
                    : Colliding.Collided)
                : Colliding.NoCollide;
        }

        private static Colliding PlayerColliding(Player player, InGameEngine engine, Direction direction,
            NormalMoveAnimation animation)
        {
            if (engine.Field.Objects.Where(o => o is Block)
                .Select(player.Cuts)
                .Any(cutting => (cutting != Direction.Nothing) && (animation.Direction == cutting)))
                return Colliding.Collided;
            if (engine.Field.Objects.Where(o => o is Hole || o is Bullet)
                .Select(player.Cuts)
                .Any(cutting => cutting != Direction.Nothing))
                return Colliding.PlayerVanishing;

            var mainPlayer = player as MainPlayer;
            if (mainPlayer != null)
                return (BiggerThanFieldX(mainPlayer, engine.Field) &&
                        (direction == Direction.Right)) ||
                       (BiggerThanFieldY(mainPlayer, engine.Field) &&
                        (direction == Direction.Down)) ||
                       ((mainPlayer.Position.X < 0) &&
                        (direction == Direction.Left)) ||
                       ((mainPlayer.Position.Y < 0) &&
                        (direction == Direction.Up))
                    ? Colliding.Collided
                    : Colliding.NoCollide;
            return Colliding.Nothing;
        }

        private static bool BiggerThanFieldX(GameObject obj, GameObject field)
            => (obj.Position.X > field.Size.X) || (obj.Position.X + obj.Size.X > field.Size.X);

        private static bool BiggerThanFieldY(GameObject obj, GameObject field)
            => (obj.Position.Y > field.Size.Y) || (obj.Position.Y + obj.Size.Y > field.Size.Y);
    }
}