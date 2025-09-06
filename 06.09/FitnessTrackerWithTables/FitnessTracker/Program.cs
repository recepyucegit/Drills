using System;
using System.Windows.Forms;

namespace FitnessTracker
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());  // Form1 değil, MainForm!
        }
    }
}