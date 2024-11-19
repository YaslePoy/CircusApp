using System.Windows;
using CircusApp.Pages;

namespace CircusApp
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BaseFrame.Navigate(new HelloPage());
        }
    }
}