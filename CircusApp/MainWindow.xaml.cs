using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace CircusApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplicationThemeManager.Apply(this);
            ApplicationThemeManager.Apply(
                ApplicationTheme.Dark);
        }
    }
}
