using CircusApp.DB;
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

namespace CircusApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private User _user;
        public RegisterPage(User user)
        {
            InitializeComponent();


            _user = user;
            if (_user != null) UserInsertData(_user);
            else UserInsertData(new User() { role_id = 1 });
        }


        public void UserInsertData(User data)
        {
            loginTB.Text = (data.login ?? "").ToString();
            passwordTB.Text = (data.password ?? "").ToString();
            surnameTB.Text = (data.surname ?? "").ToString();
            nameTB.Text = (data.name ?? "").ToString();
            roleCB.SelectedIndex = data.role_id - 1;

        }

        public bool UserFetchData(User data)
        {
            data.login = loginTB.Text;
            data.password = passwordTB.Text;
            data.surname = surnameTB.Text;
            data.name = nameTB.Text;
            data.role_id = roleCB.SelectedIndex + 1;
            return true;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                _user = new User();
                if (UserFetchData(_user))
                {
                    App.DB.User.Add(_user);
                    App.DB.SaveChanges();
                    NavigationService.Navigate(new EmployeesPage());
                    return;
                }
            }

            if (UserFetchData(_user))
            {
                App.DB.SaveChanges();
                NavigationService.Navigate(new EmployeesPage());

            }

        }
    }
}
