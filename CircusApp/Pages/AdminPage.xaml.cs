using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CircusApp.DB;

namespace CircusApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
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
            AdminNav.Navigate(new TicketPage(new Hall { columns = 100, rows = 5 }));
        }
    }
}
