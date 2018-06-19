using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LovePlus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static BAL.Logic logic = new BAL.Logic();
        public static Frame MFrame;
        public MainWindow()
        {
            InitializeComponent();
            MFrame = MainFrame;
            SetFrame("Pages/LoginPage.xaml");
        }
        public static void SetFrame(string Path)
        { MFrame.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { MFrame.Source = new Uri(Path, UriKind.RelativeOrAbsolute); })); }
    }
}
