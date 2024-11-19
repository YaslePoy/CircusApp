using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CircusApp.DB;

namespace CircusApp.Pages
{
    /// <summary>
    ///     Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            EmpDG.ItemsSource = App.DB.User.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage(null));
        }

        private void EmpDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EmpDG.SelectedValue is User u && u.role_id == 2)
                NavigationService.Navigate(new ArtistPage(u));
            else
                NavigationService.Navigate(new RegisterPage(EmpDG.SelectedValue as User));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button).DataContext as User;
            if (user == null)
                return;
            App.DB.User.Remove(user);
            App.DB.SaveChanges();
            EmpDG.ItemsSource = null;
            EmpDG.ItemsSource = App.DB.User.ToList();
        }
    }
}