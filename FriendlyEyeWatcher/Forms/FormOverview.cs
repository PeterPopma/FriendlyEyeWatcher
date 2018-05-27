using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriendlyEyeWatcher.Forms
{
    public partial class FormOverview : Form
    {
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndIntertAfter, int X, int Y, int cx, int cy, int uFlags);
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int Which);

        private static System.Windows.Forms.Timer updateScreenTimer;

        public FormOverview()
        {
            InitializeComponent();
            SetupTimer();
            // Go Fullscreen
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            //            SetWindowPos(Handle, IntPtr.Zero, 0, 0, GetSystemMetrics(0), GetSystemMetrics(1), 64);
        }

        private void SetupTimer()
        {
            // Create a timer with a 100 msec interval.
            updateScreenTimer = new System.Windows.Forms.Timer();
            updateScreenTimer.Interval = 4000;
            updateScreenTimer.Tick += new EventHandler(OnTimedEventUpdateScreen);
            updateScreenTimer.Start();
        }

        private void OnTimedEventUpdateScreen(object sender, EventArgs eArgs)
        {
            Close();
        }
    }
}
