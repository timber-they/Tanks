using System.CodeDom;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Painting.Types.Paint;
using Tanks.Enums;
using Tanks.Frontend.UI;
using Tanks.Objects.Animation;
using Tanks.Objects.GameObjects;

namespace Tanks.Backend
{
    public class InGameEngine
    {
        private decimal _currentId;

        public Field Field;
        public readonly MainWindow Window;
        private const bool Debugging = false;

        public InGameEngine(MainWindow window)
        {
            Window = window;
            Init();
        }

        public ObservableCollection<Animation> Animations { get; set; }
        private ObservableCollection<Keys> PressedKeys { get; set; }

        public decimal CurrentId
        {
            get
            {
                _currentId++;
                return _currentId;
            }
        }

        public MainPlayer Player
        {
            get { return Field.GetMainPlayer; }
            private set
            {
                for (var i = 0; i < Field.Objects.Count; i++)
                    if (Field.Objects[i] is MainPlayer)
                        Field.Objects[i] = value;
                Window.Refresh();
            }
        }

        public void Paint(Graphics p)
        {
            Field.Paint(p);
            if (!Debugging) return;
            foreach (var o in Field.Objects)
            {
                p.DrawRectangle(Pens.Red, o.Position.X, o.Position.Y, o.Size.X, o.Size.Y);
                p.FillEllipse(new SolidBrush(Color.Red), o.CenterPosition.X, o.CenterPosition.Y, 5, 5);
            }
        }

        public void OnTick()
        {
            Animations =
                new ObservableCollection<Animation>(
                    Animations.Where(animation => !(animation.AnimatedObject is MainPlayer)));
            foreach (var key in PressedKeys)
                Handler.KeyInPutHandler(this, key, KeyHandlerAction.Down);
            if (!Player.Lives.Equals(Window.LiveIndicator.Text.Length))
                Window.LiveIndicator.Text = new string('♥', Player.Lives);
            if (Animations.Count <= 0)
                return;
            Animations = AnimationUpdating.UpdateAnimations(Animations, this);
            Field.Objects = new ObservableCollection<GameObject>(
                Field.Objects.Where(
                    o =>
                        !((o is Bullet || o is Explosion || o is Mine) &&
                          Animations.All(animation => animation.AnimatedObject.Id != o.Id))));
            foreach (var animation in Animations)
                animation.Animate();
            Window.Refresh();
        }

        public void OnEvilTick()
        {
            var col = Field.Objects.Where(o => o is NormalEvilPlayer).ToList();
            foreach (NormalEvilPlayer e in col)
                e.DoSomething(this);
            Window.Refresh();
        }

        private void Init()
        {
            PressedKeys = new ObservableCollection<Keys>();
            Field = new Field(new Coordinate(0, 0), new Coordinate(Window.Width, Window.Height),
                new ObservableCollection<GameObject>(), CurrentId);
            Field.AddObject(AddableObjects.MainPlayer, this);
            Field.AddObject(AddableObjects.DestroyableBlock, this, new Coordinate(500, 500));
            Field.AddObject(AddableObjects.Hole, this, new Coordinate(1000, 500));
            Field.AddObject(AddableObjects.NormalBlock, this, new Coordinate(700,700));
            Field.AddObject(AddableObjects.NormalEvilPlayer, this, new Coordinate(1200,400));
            Animations = new ObservableCollection<Animation>();
        }

        public void OnMouseMove(Coordinate position)
        {
            Task.Run(async () => await Task.Run(() =>
            {
                Tracer.TracePosition(position, Player);
                MethodInvoker invoker = delegate { Player = Player; };
                if (Window.InvokeRequired)
                    Window.Invoke(invoker);
                else
                    invoker();
            }));
        }

        public void OnKeyDown(PreviewKeyDownEventArgs e)
        {
            Handler.KeyInPutHandler(this, e.KeyCode, KeyHandlerAction.Down);
            if (!PressedKeys.Contains(e.KeyData))
                PressedKeys.Add(e.KeyData);
        }

        public void OnKeyUp(KeyEventArgs e)
        {
            Handler.KeyInPutHandler(this, e.KeyCode, KeyHandlerAction.Up);
            while (PressedKeys.Contains(e.KeyData))
                PressedKeys.Remove(e.KeyData);
            if (e.KeyData != Keys.Space) return;
            Field.AddObject(AddableObjects.Mine, this, Player.CenterPosition);
            var obj = Field.Objects.Last();
            obj.Position = obj.Position.Sub(obj.Size.Div(2));
        }
    }
}