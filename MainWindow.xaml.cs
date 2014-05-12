using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace VirtualKeyboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ArrayList Layouts = new ArrayList();
        //===
        public MainWindow()
        {
            InitializeComponent();
            Scan();
            RenderButtons();
        }
        public void Scan()
        {
            string[] files=Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xaml");
            foreach (string path in files)
            {
                try
                {
                    Window W = Lib.GetWindowFromXAML(path);
                    Layouts.Add(W);
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
                }
            }
        }
        public void BindActions(Window w)
        {
            ArrayList controls = Lib.GetComponentFromWindow(w);
            foreach (object obj in controls)
            {
                Button button = obj as Button;
                if (button == null) continue;
                string tag = (string)button.Tag;
                try
                {
                    int code = int.Parse(tag);
                    WindowsInput.VirtualKeyCode vkc = (WindowsInput.VirtualKeyCode)code;
                    button.Click += (o, k) => Lib.IS_Press(vkc);
                }
                catch (Exception E)
                {
                }
            }
        }
        public void RenderButtons()
        {
            foreach (Window layout in Layouts)
            {
                string title = layout.Title;
                Button B = new Button();
                B.Content = title;
                B.Click += (o, k) => { BindActions(layout);  layout.Show(); };
                grid.Children.Add(B);
            }
        }
    }
}
