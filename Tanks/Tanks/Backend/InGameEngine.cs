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
using Tanks.Objects.GameObjects;

namespace Tanks.Backend
{
    public class InGameEngine
    {
        public MainWindow Window;

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
            if (!Player.Lives.Equals(Window.LiveIndicator.Text.Length))
                Window.LiveIndicator.Text = new string('♥', Player.Lives);
            if (Animations.Count <= 0)
                return;
            Animations = Collision.UpdateAnimations(Animations, this);
            Field.Objects = new ObservableCollection<GameObject> (
                Field.Objects.Where (
                    o => !(o is Bullet && Animations.All (animation => animation.AnimatedObject.Id != o.Id))));
            foreach (var animation in Animations)
                animation.AnimateMovement ();
            Window.Refresh ();
        }

        public InGameEngine (MainWindow window)
        {
            Window = window;
            Init ();
        }

        public void Init ()
        {
            Field = new Field (new Coordinate (0, 0), new Coordinate (Window.Width, Window.Height), new ObservableCollection<GameObject> (), CurrentId);
            Field.AddObject (AddableObjects.MainPlayer, this);
            Field.AddObject(AddableObjects.NormalBlock, this, new Coordinate(500,500));
            Field.AddObject(AddableObjects.Hole, this, new Coordinate(1000,500));
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
                Window.Refresh ();
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
                  Window.Invoke (invoker);
              }));
        }
    }
}