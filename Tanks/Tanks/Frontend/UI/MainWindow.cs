using System;
using System.Windows.Forms;
using Painting.Types.Paint;
using Tanks.Backend;

namespace Tanks.Frontend.UI
{
    public partial class MainWindow : Form
    {
        private InGameEngine _inGameEngine;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e) => _inGameEngine.Paint(e.Graphics);

        private void MainWindow_Tick(object sender, EventArgs e) => _inGameEngine.OnTick();

        private void MainWindow_MouseClick(object sender, MouseEventArgs e)
            => Handler.MouseInputHandler(e.Button, _inGameEngine);

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
            => _inGameEngine.OnMouseMove(new Coordinate(e.X, e.Y));

        private void MainWindow_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
            => _inGameEngine.OnKeyDown(e);

        private void MainWindow_KeyUp(object sender, KeyEventArgs e) => _inGameEngine.OnKeyUp(e);

        private void MainWindow_Load(object sender, EventArgs e)
        {
            MinimumSize = Size;
            MaximumSize = Size;
            _inGameEngine = new InGameEngine(this);
        }
    }
}