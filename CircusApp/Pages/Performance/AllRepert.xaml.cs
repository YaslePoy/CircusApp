using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace CircusApp.Pages.Performance
{
    public partial class AllRepert : Page
    {
        public AllRepert()
        {
            InitializeComponent();
            PerfDG.ItemsSource = App.DB.Repertoire.ToList();
        }

        private void PerfDG_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RepertPage(PerfDG.SelectedItem as DB.Repertoire));
        }
    }
}