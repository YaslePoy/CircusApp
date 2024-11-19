using System.Windows;
using System.Windows.Controls;
using CircusApp.DB;

namespace CircusApp.Pages
{
    /// <summary>
    ///     Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private User _user;

        public RegisterPage(User user)
        {
            InitializeComponent();


            _user = user;
            if (_user != null) UserInsertData(_user);
            else UserInsertData(new User { role_id = 1 });
        }


        public void UserInsertData(User data)
        {
            loginTB.Text = (data.login ?? "");
            passwordTB.Text = (data.password ?? "");
            surnameTB.Text = (data.surname ?? "");
            nameTB.Text = (data.name ?? "");
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