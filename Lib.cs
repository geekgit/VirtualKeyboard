using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
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
        public static Window GetWindowFromXAML(string XAMLPath)
        {
            var reader = new StreamReader(XAMLPath);
            var xmlReader = XmlReader.Create(reader);
            var newWindow = XamlReader.Load(xmlReader) as Window;
            return newWindow;
        }
        public static ArrayList GetComponentFromWindow(Window w)
        {
            ArrayList list=new ArrayList();
            GetAllComponents_sub(w, list);
            return list;
        }
       private static void GetAllComponents_sub(DependencyObject DO, ArrayList list)
        {
            var children = LogicalTreeHelper.GetChildren(DO);
            foreach (var child in children)
            {
                list.Add(child);
                var DepObj = child as DependencyObject;
                if (DepObj == null) continue;
                if (DepObj != null)
                {
                    GetAllComponents_sub(DepObj, list);
                }
            }
        }
    }
}
