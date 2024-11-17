using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tobii.Interaction;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;


using System.IO;
using System.IO.Pipes;


namespace Tobii_Cursor
{
    public partial class Form1 : Form
    {

        #region Dll Imports
        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String sClassName, String sAppName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();


        #endregion






        public static bool WaitForInputIdle(IntPtr hWnd, int timeout = 0)
        {
            int pid;
            int tid = GetWindowThreadProcessId(hWnd, out pid);
            if (tid == 0) throw new ArgumentException("Window not found");
            var tick = Environment.TickCount;
            do
            {
                if (IsThreadIdle(pid, tid)) return true;
                System.Threading.Thread.Sleep(15);
            } while (timeout > 0 && Environment.TickCount - tick < timeout);
            return false;
        }

        private static bool IsThreadIdle(int pid, int tid)
        {
            Process prc = System.Diagnostics.Process.GetProcessById(pid);

            var thr = prc.Threads.Cast<ProcessThread>().First((t) => tid == t.Id);
            return thr.ThreadState == System.Diagnostics.ThreadState.Wait &&
                   thr.WaitReason == ThreadWaitReason.UserRequest;
        }

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int pid);


//Extra Selects Code
/*
        public void setupExtraSelects()
        {

            double radiusX = ((double)30 / Screen.PrimaryScreen.Bounds.Width) * 65536;
            double radiusY = ((double)30 / Screen.PrimaryScreen.Bounds.Height) * 65536;
            int segments = 6;
            int extraRotation = 30;

            for (int i = 0; i < segments; ++i)
            {
                double x = radiusX * Math.Cos((((360 / segments) * i) + extraRotation) * Math.PI / 180);
                double y = radiusY * Math.Sin((((360 / segments) * i) + extraRotation) * Math.PI / 180);
                Program.extraSelects.AddLast(new Point((int)x, (int)y));
            }

            extraRotation = 0;
            radiusX *= 2;
            radiusY *= 2;
            segments = 12;
            for (int i = 0; i < segments; ++i)
            {
                double x = radiusX * Math.Cos((((360 / segments) * i) + extraRotation) * Math.PI / 180);
                double y = radiusY * Math.Sin((((360 / segments) * i) + extraRotation) * Math.PI / 180);
                Program.extraSelects.AddLast(new Point((int)x, (int)y));
            }
        }
*/







        Host host = new Host();
        GazePointDataStream gazePointDataStream;


        FormOverlay frmOverlay = new FormOverlay();

        public delegate void testDelegate();

        private System.Windows.Forms.Timer timer;
        private NamedPipeServerStream pipeServer;
        private StreamWriter pipeWriter;
        double gazeXpos, gazeYpos = 0;

 









        public Form1()
        {
            InitializeComponent();
        }












        Thread serverThread;

        private void Form1_Load(object sender, EventArgs e)
        {
            StartServerThread();



            gazePointDataStream = host.Streams.CreateGazePointDataStream();
            gazePointDataStream.GazePoint((double gazePointX, double gazePointY, double _) => FillCoords(gazePointX, gazePointY));



            if (chkOverlay.Checked) {frmOverlay.Show();}
        }

        private void StartServerThread()
        {
            // Start the server in a separate thread
            Console.WriteLine("Starting Server");
            serverThread = new Thread(StartServer);
            serverThread.IsBackground = true;
            serverThread.Start();
        }


        private void StartServer()
        {
            try
            {
                // Initialize named pipe server
                pipeServer = new NamedPipeServerStream("mypipe", PipeDirection.InOut);
                pipeServer.WaitForConnection(); // Wait for client connection

                // Initialize pipe writer
                pipeWriter = new StreamWriter(pipeServer);

                // Marshal timer operations onto the UI thread
                this.Invoke((MethodInvoker)delegate {
                    // Start sending coordinates at an interval
                    StartSendingCoordinates();
                });
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show("Error starting server: " + ex.Message);
            }
        }



        private void StartSendingCoordinates()
        {
            // Initialize timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 12; // Interval in milliseconds (adjust as needed)
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Format coordinates
            string coordinates = $"X:{gazeXpos}, Y:{gazeYpos}";

            // Send coordinates to the named pipe
            SendCoordinates(coordinates);

        }

        

        private void SendCoordinates(string coordinates)
        {
            try
            {
                pipeWriter.WriteLine(coordinates);
                pipeWriter.Flush();
            }
            catch (IOException ex)
            {
                //Usually when the try fails it is because the client lost connection. Need to reestablish the connection by restarting the server thread
                timer.Dispose();
                pipeServer.Dispose();
                StartServerThread();


                // Handle pipe communication errors
                Console.WriteLine("Error sending coordinates: " + ex.Message);
            }
        }






















        private void FillCoords(double Xpos, double Ypos)
        {
            gazeXpos = Xpos;
            gazeYpos = Ypos;


            if (this.chkSmoothing.Checked)
            {
                Program.eyeXposList.AddLast(Xpos - frmOverlay.Left);
                Program.eyeYposList.AddLast(Ypos - frmOverlay.Top);
                Program.eyeXpos = SmoothTobiiEye(ref Program.eyeXposList, 40);
                Program.eyeYpos = SmoothTobiiEye(ref Program.eyeYposList, 40);
            }
            else
            {
                Program.eyeXpos = Xpos;
                Program.eyeYpos = Ypos;
            }
        }

        //Filters out jerky Tobii Eye movements and returns the average position
        //Tobii points tend to be scattered so to make the cursor look more fluid
        //we do some point averaging.  If it detects that the eye is moving to a new 
        //location then it removes old stored points.
        private double SmoothTobiiEye(ref LinkedList<double> list, int Threshold)
        {
            if (list.Count > 2)
            {
                if (list.ElementAt(list.Count - 1) > list.ElementAt(0) + Threshold ||
                    list.ElementAt(list.Count - 1) < list.ElementAt(0) - Threshold)
                {
                    for (int iCount = list.Count - 2; iCount >= 1; --iCount)
                    {
                        if (list.ElementAt(iCount) > list.ElementAt(iCount - 1) + Threshold ||
                        list.ElementAt(iCount) < list.ElementAt(iCount - 1) - Threshold)
                        {
                            list.Remove(list.ElementAt(iCount - 1));
                        }
                    }
                }
            }

            double averagePos = 0;
            for (int iCount = list.Count - 1; iCount >= 0; --iCount)
            { averagePos += list.ElementAt(iCount); }

            if (list.Count > 0)
                averagePos /= list.Count;

            if (list.Count > 9)
            { list.RemoveFirst(); }

            return averagePos;
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            host.Dispose();
            Environment.Exit(Environment.ExitCode);
        }

        private void chkOverlay_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOverlay.Checked)
            {
                frmOverlay.Show();
            }
            else
            {
                frmOverlay.Hide();
            }
        }





    }
}
