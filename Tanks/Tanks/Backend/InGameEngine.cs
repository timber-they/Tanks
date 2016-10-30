using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Painting.Types.Paint;
using Tanks.Enums;
using Tanks.Frontend.UI;
using Tanks.Objects;
using Tanks.Objects.Animation;

namespace Tanks.Backend
{
    public class InGameEngine
    {
        public MainWindow Form;

        public Field Field;
        private decimal _currentId;
        public ObservableCollection<Animation> Animations { get; set; }

        public decimal CurrentId
        {
            get
            {
                _currentId++;
                return _currentId;
            }
        }

        public void Paint (Graphics p) => Field.View.Paint (p, Field.Position.Add (Field.Size.Div (2)));

        public void OnTick ()
        {
            if (Animations.Count <= 0)
                return;
            Animations =
                new ObservableCollection<Animation> (
                    Animations.Where (
                        animation =>
                            !(animation.AnimatedObject is Bullet && (BiggerThanFieldX(animation.AnimatedObject) || BiggerThanFieldY(animation.AnimatedObject) || animation.AnimatedObject.Position.X < 0 || animation.AnimatedObject.Position.Y < 0)) &&
                            !(animation.AnimatedObject is Player &&
                              (BiggerThanFieldX(animation.AnimatedObject) && ((NormalMoveAnimation) animation).Direction == Direction.Right || 
                               BiggerThanFieldY(animation.AnimatedObject) && ((NormalMoveAnimation) animation).Direction == Direction.Down ||
                               animation.AnimatedObject.Position.X < 0 && ((NormalMoveAnimation)animation).Direction == Direction.Left ||
                               animation.AnimatedObject.Position.Y < 0 && ((NormalMoveAnimation)animation).Direction == Direction.Up))));
            Field.Objects = new ObservableCollection<GameObject> (
                Field.Objects.Where (
                    o => !(o is Bullet && Animations.All (animation => animation.AnimatedObject.Id != o.Id))));
            foreach (var animation in Animations)
                animation.AnimateMovement ();
            Form.Refresh ();
        }

        private bool BiggerThanFieldX(GameObject obj)
            => obj.Position.X > Field.Size.X || obj.Position.X + obj.Size.X > Field.Size.X;

        private bool BiggerThanFieldY(GameObject obj)
            => obj.Position.Y > Field.Size.Y || obj.Position.Y + obj.Size.Y > Field.Size.Y;

        public InGameEngine (MainWindow form)
        {
            Form = form;
            Init ();
        }

        public void Init ()
        {
            Field = new Field (new Coordinate (0, 0), new Coordinate (Form.Width - 50, Form.Height - 50), new ObservableCollection<GameObject> (), CurrentId);
            Field.AddObject (AddableObjects.MainPlayer, this);
            Animations = new ObservableCollection<Animation> ();
        }

        public MainPlayer Player
        {
            get { return Field.GetMainPlayer; }
            set
            {
                for (var i = 0; i < Field.Objects.Count; i++)
                    if (Field.Objects[i] is MainPlayer)
                        Field.Objects[i] = value;
                Form.Refresh ();
            }
        }

        public void OnMouseMove (Coordinate position)
        {
            Task.Run (async () => await Task.Run (() =>
              {
                  Tracer.TraceMouse (position, Player);
                  MethodInvoker invoker = delegate ()
                  {
                      Player = Player;
                  };
                  Form.Invoke (invoker);
              }));
        }
    }
}