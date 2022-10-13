using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;

namespace PrintSSaver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application. Also for preventing another instances.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string appGuid = ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), true)[0]).Value;
            using (Mutex mutex = new Mutex(false, appGuid))
            {
                Random random = new Random();
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show(appRunClishe[random.Next(7)]);
                    return;
                }
                GC.Collect();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PrintSSaver());
            }
        }
        //private static string appGuid = "7e39a96c-cb68-42eb-8fa6-0fcfe3350563";
        private static string[] appRunClishe = new string[] { "Instance already running.", "Application is already running.", "Can't see the app?\nHave you considered checking the taskbar?", "No.\nIt's there.", "*sigh*\nDon't spam.", "You Can (Not) Advance.", "Thee shalt not be permitted to parallel summon." };
    }
}
