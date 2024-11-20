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
    /// Логика взаимодействия для EventsPage.xaml
    /// </summary>
    public partial class EventsPage : Page
    {
        public EventsPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTB.Text))
                return;
            if (string.IsNullOrWhiteSpace(descriprionTB.Text))
                return;
            if (string.IsNullOrWhiteSpace(addressTB.Text))
                return;
            if (timestampDP.SelectedDate is null)
                return;
            var ev = new DB.Event()
            {
                address = addressTB.Text,
                descriprion = descriprionTB.Text,
                hallId = hallIdTB.SelectedIndex + 1,
                name = nameTB.Text,
                timestamp = timestampDP.SelectedDate.GetValueOrDefault()
            };
            App.DB.Event.Add(ev);
            App.DB.SaveChanges();
            MessageBox.Show("Событие успешно добавлено");
        }
    }
}
