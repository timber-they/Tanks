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
                new MainPlayer(new Coordinate(50, 50), new Coordinate(50, 50), 0, 3),
                new NormalBullet(new Coordinate(100,100), new Coordinate(10,30), 0)
            });

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            f.View.Paint(e.Graphics);
        }
    }
}
