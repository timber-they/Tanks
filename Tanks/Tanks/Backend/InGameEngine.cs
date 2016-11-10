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
using Tanks.Objects.GameObjects.Evil;

namespace Tanks.Backend
{
    public class InGameEngine
    {
        private decimal _currentId;

        public Field Field;
        public readonly MainWindow Window;
        public bool Debugging = true;
        public Coordinate MousePosition;

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

        public MainPlayer MainPlayer
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
                p.FillEllipse(new SolidBrush(Color.Red), o.CenterPosition().X, o.CenterPosition().Y, 5, 5);
            }
        }

        public void OnTick()
        {
            Animations =
                new ObservableCollection<Animation>(
                    Animations.Where(animation => !(animation.AnimatedObject is MainPlayer)));
            foreach (var key in PressedKeys)
                Handler.KeyInPutHandler(this, key, KeyHandlerAction.Down);
            if (!MainPlayer.Lives.Equals(Window.LiveIndicator.Text.Length))
                Window.LiveIndicator.Text = new string('♥', MainPlayer.Lives);
            var col = Field.Objects.Where(o => o is EvilPlayer).ToList();
            foreach (var e in col)
                ((EvilPlayer)e).DoSomething();
            if (Animations.Count <= 0)
            {
                Window.Refresh();
                return;
            }
            Animations = AnimationUpdating.UpdateAnimations(Animations, this);
            Field.Objects = new ObservableCollection<GameObject>(
                Field.Objects.Where(
                    o =>
                        !((o is Bullet || o is Explosion || o is Mine) &&
                          Animations.All(animation => animation.AnimatedObject.Id != o.Id))));
            foreach (var animation in Animations)
                animation.Animate(this);
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
            Field.AddObject(AddableObjects.EvilPlayer, this, new Coordinate(1200,400), 1);
            Animations = new ObservableCollection<Animation>();
        }

        public void OnMouseMove(Coordinate position)
        {
            Task.Run(async () => await Task.Run(() =>
            {
                MousePosition = position;
                Tracer.TracePosition(position, MainPlayer);
                MethodInvoker invoker = delegate { MainPlayer = MainPlayer; };
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
            Field.AddObject(AddableObjects.Mine, this, MainPlayer.CenterPosition());
            var obj = Field.Objects.Last();
            obj.Position = obj.Position.Sub(obj.Size.Div(2));
        }
    }
}