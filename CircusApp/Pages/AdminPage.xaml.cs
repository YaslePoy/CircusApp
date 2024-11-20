using System.Windows.Controls;
using System.Windows.Input;
using CircusApp.DB;
using CircusApp.Pages.Performance;

namespace CircusApp.Pages
{
    /// <summary>
    ///     Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void UserRegister(object sender, MouseButtonEventArgs e)
        {
            AdminNav.Navigate(new EmployeesPage());
        }

        private void UserRegisterTalant(object sender, MouseButtonEventArgs e)
        {
            AdminNav.Navigate(new TalantPage());
        }

        private void ShowTicketPage(object sender, MouseButtonEventArgs e)
        {
            AdminNav.Navigate(new PreTicketPage());
        }

        private void EventCreatePage(object sender, MouseButtonEventArgs e)
        {
            AdminNav.Navigate(new EventsPage());
        }

        private void StatsPage(object sender, MouseButtonEventArgs e)
        {
            AdminNav.Navigate(new StatisticsPage());
        }

        private void PerformancesPage(object sender, MouseButtonEventArgs e)
        {
            AdminNav.Navigate(new AllPerformancePage());
        }

        private void RepertPage(object sender, MouseButtonEventArgs e)
        {
            AdminNav.Navigate(new AllRepert());
            
        }
    }
}