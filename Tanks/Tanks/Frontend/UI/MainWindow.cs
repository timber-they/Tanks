using System;
using System.Windows.Forms;
using Painting.Types.Paint;
using Tanks.Backend;
using Tanks.Enums;

namespace Tanks.Frontend.UI
{
    public partial class MainWindow : Form
    {
        public InGameEngine InGameEngine;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e) => InGameEngine.Paint(e.Graphics);

        private void MainWindow_Tick (object sender, EventArgs e) => InGameEngine.OnTick();

        private void MainWindow_MouseClick (object sender, MouseEventArgs e) => Handler.MouseInputHandler(e.Button, InGameEngine);

        private void MainWindow_MouseMove(object sender, MouseEventArgs e) => InGameEngine.OnMouseMove(new Coordinate(e.X, e.Y));

        //private void MainWindow_KeyDown(object sender, KeyEventArgs e) => Handler.KeyInPutHandler(InGameEngine, e.KeyCode, KeyHandlerAction.Down);

        private void MainWindow_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
            => InGameEngine.OnKeyDown(e);

        private void MainWindow_KeyUp(object sender, KeyEventArgs e) => InGameEngine.OnKeyUp(e);

        private void MainWindow_Load (object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            InGameEngine = new InGameEngine (this);
        }
    }
}
