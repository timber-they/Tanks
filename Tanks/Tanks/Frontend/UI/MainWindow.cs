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
            InGameEngine = new InGameEngine(this);
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e) => InGameEngine.Paint(e.Graphics);

        private void MainWindow_Tick (object sender, EventArgs e) => InGameEngine.OnTick();

        private void MainWindow_MouseClick (object sender, MouseEventArgs e) => InGameEngine.OnClick();

        private void MainWindow_MouseMove(object sender, MouseEventArgs e) => InGameEngine.OnMouseMove(new Coordinate(e.X, e.Y));

        private void MainWindow_KeyDown(object sender, KeyEventArgs e) => Handler.KeyInPutHandler(InGameEngine, e.KeyCode, KeyHandlerAction.Down);

        private void MainWindow_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
            => Handler.KeyInPutHandler(InGameEngine, e.KeyCode, KeyHandlerAction.Down);

        private void MainWindow_KeyUp(object sender, KeyEventArgs e) => Handler.KeyInPutHandler(InGameEngine, e.KeyCode, KeyHandlerAction.Up);
    }
}
