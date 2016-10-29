using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Painting.Types.Paint;
using Tanks.Objects;

namespace Tanks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Field f = new Field(new Coordinate(0, 0), new Coordinate(500, 500),
            new ObservableCollection<GameObject>
            {
                new MainPlayer(new Coordinate(50, 50), new Coordinate(500, 500), 0, 3),
                new NormalBullet(new Coordinate(1000,50), new Coordinate(100,300), 0)
            });

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            f.View.Paint(e.Graphics, f.Position.Add(f.Size.Div(2)));
        }

        private void timer1_Tick (object sender, EventArgs e)
        {
            f.Objects[0].Rotation = (f.Objects[0].Rotation + 1f) % 360;
            f.Objects[1].Rotation = (f.Objects[1].Rotation + 1f) % 360;
            Refresh ();
        }

        private void Form1_MouseClick (object sender, MouseEventArgs e)
        {
            timer1_Tick(sender, null);
        }
    }
}
