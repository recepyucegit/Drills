using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System;
using System.Windows.Forms;
using FitnessTracker; // 👈 Namespace'in doğru olduğundan emin ol

namespace FitnessTracker
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm()); // ✅ MainForm olarak değiştirildi
        }
    }
}
