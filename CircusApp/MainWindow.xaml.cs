using Wpf.Ui.Appearance;

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
                ApplicationTheme.Dark                                      // Whether to change accents automatically
            );
        }
    }
}
