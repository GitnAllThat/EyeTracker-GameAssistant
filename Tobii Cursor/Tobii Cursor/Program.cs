using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tobii.Interaction;
using System.Drawing;

namespace Tobii_Cursor
{

    static class Program
    {
        public static IntPtr formOverlayHandle;
        public static double eyeXpos;
        public static double eyeYpos;


        public static LinkedList<double> eyeXposList = new LinkedList<double>();
        public static LinkedList<double> eyeYposList = new LinkedList<double>();



        public static Form1 frm1;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frm1 = new Form1();
            Application.Run(frm1);
        }
    }
}
