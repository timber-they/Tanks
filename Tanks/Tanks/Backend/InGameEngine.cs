using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Painting.Types.Paint;
using Tanks.Frontend.UI;
using Tanks.Objects;

namespace Tanks.Backend
{
    public class InGameEngine
    {
        public MainWindow Form;

        public Field Field;
        private decimal _currentId;
        public ObservableCollection<Animation> Animations { get; set; }
        private bool TickRefresh { get; set; }

        public decimal CurrentId
        {
            get
            {
                _currentId++;
                return _currentId;
            }
        }

        public void Paint (Graphics p)
        {
            Field.View.Paint (p, Field.Position.Add (Field.Size.Div (2)));
            if (!TickRefresh)
                return;
            foreach (var animation in Animations)
                animation.AnimateMovement(p);
            TickRefresh = false;
        }

        public void OnTick ()
        {
            if (Animations.Count <= 0)
                return;
            TickRefresh = true;
            Form.Refresh ();
        }

        public void OnClick ()
        {
        }

        public InGameEngine (MainWindow form)
        {
            Form = form;
            Init ();
        }

        public void Init ()
        {
            Field = new Field (new Coordinate (0, 0), new Coordinate (Form.Width - 50, Form.Height - 50), new ObservableCollection<GameObject> (), CurrentId);
            Field.AddMainPlayer (CurrentId);
            Animations = new ObservableCollection<Animation> ();
        }

        public MainPlayer Player
        {
            get { return Field.Objects.FirstOrDefault (o => o is MainPlayer) as MainPlayer; }
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
            Task.Run(async () => await Task.Run(() =>
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