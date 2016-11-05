namespace Tanks.Frontend.UI
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.LiveIndicator = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.MainWindow_Tick);
            // 
            // LiveIndicator
            // 
            this.LiveIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LiveIndicator.AutoSize = true;
            this.LiveIndicator.Font = new System.Drawing.Font("Consolas", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LiveIndicator.ForeColor = System.Drawing.Color.Red;
            this.LiveIndicator.Location = new System.Drawing.Point(12, 220);
            this.LiveIndicator.Name = "LiveIndicator";
            this.LiveIndicator.Size = new System.Drawing.Size(0, 32);
            this.LiveIndicator.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(284, 261);
            Controls.Add(this.LiveIndicator);
            Cursor = System.Windows.Forms.Cursors.Cross;
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "MainWindow";
            Text = "Tanks";
            //this.TopMost = true;
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += new System.EventHandler(this.MainWindow_Load);
            Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            //this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyDown);
            KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseClick);
            MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainWindow_PreviewKeyDown);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Label LiveIndicator;
    }
}

