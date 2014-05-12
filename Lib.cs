using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsInput;

namespace VirtualKeyboard
{
    public static class Lib
    {

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);
        public static void Send_SendAPI(string process_name, string key)
        {
            Process p = Process.GetProcessesByName(process_name).FirstOrDefault();
            if (p != null)
            {

                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                System.Windows.Forms.SendKeys.SendWait(key);
            }
            else
            {
                MessageBox.Show("process null!");
            }
        }
        public static void IS_Press(VirtualKeyCode vkc)
        {

            InputSimulator.SimulateKeyDown(vkc);
            Thread.Sleep(200);
            InputSimulator.SimulateKeyUp(vkc);
        }
    }
}
