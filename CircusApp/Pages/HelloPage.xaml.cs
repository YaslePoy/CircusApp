using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CircusApp.Pages
{
    public partial class HelloPage : Page
    {
        public HelloPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var l = LoginTB.Text;
            var p = PassPB.Password;
            if (string.IsNullOrWhiteSpace(l))
            {
                MessageBox.Show("Не может быть такого логина");
                return;
            }

            if (string.IsNullOrWhiteSpace(p))
            {
                MessageBox.Show("Не может быть такого пароля");
                return;
            }

            var user = App.DB.User.FirstOrDefault(i => i.login == l && i.password == p);
            if (user is null) MessageBox.Show("Проверьте правильность введенного логина и пароля");

            App.User = user;

            switch (user.role_id)
            {
                case 1:
                    NavigationService.Navigate(new AdminPage());
                    break;
                case 2:
                    NavigationService.Navigate(new ArtistHelloPage());
                    break;
                case 3:
                    NavigationService.Navigate(new ManagerPage());
                    break;
                case 4:
                    NavigationService.Navigate(new CashierPage());
                    break;
            }
        }
    }
}