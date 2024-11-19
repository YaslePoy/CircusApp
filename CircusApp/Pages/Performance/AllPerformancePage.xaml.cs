using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace CircusApp.Pages.Performance
{
    public partial class AllPerformancePage : Page
    {
        public AllPerformancePage()
        {
            InitializeComponent();
            PerfDG.ItemsSource = App.DB.Performance.ToList();
        }

        private void PerfDG_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new PerformancePage(PerfDG.SelectedItem as DB.Performance));
        }
    }
}