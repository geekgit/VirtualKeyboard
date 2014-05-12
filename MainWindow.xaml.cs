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
        public Window GetWindowFromXAML(string XAMLPath)
        {
            var reader = new StreamReader(XAMLPath);
            var xmlReader = XmlReader.Create(reader);
            var newWindow = XamlReader.Load(xmlReader) as Window;
            return newWindow;
        }
        public void Scan()
        {
            string[] files=Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xaml");
            foreach (string path in files)
            {
                try
                {
                    Window W = GetWindowFromXAML(path);
                    Layouts.Add(W);
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message);
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
                B.Click += (o, k) => layout.Show();
                grid.Children.Add(B);
            }
        }
    }
}
