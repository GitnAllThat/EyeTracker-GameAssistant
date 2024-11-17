using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Tobii_Cursor
{
    public partial class FormOverlay : Form
    {

        #region Dll Imports
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        #endregion



        public const string WINDOW_NAME = "Untitled - Paint";
        IntPtr handle = FindWindow(null, WINDOW_NAME);



        public struct RECT
        {
            public int left, top, right, bottom;
        }






        public FormOverlay()
        {
            InitializeComponent();
        }

        private void FormOverlay_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
          

            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);




            this.WindowState = FormWindowState.Maximized;

            Program.formOverlayHandle = this.handle;
        }









        



        private void timer1_Tick(object sender, EventArgs e)
        {

            this.imgCursor.Left = (int)Program.eyeXpos;
            this.imgCursor.Top = (int)Program.eyeYpos;
        }


    }
}
