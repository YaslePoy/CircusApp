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
    /// Логика взаимодействия для PreTicketPage.xaml
    /// </summary>
    public partial class PreTicketPage : Page
    {
        public PreTicketPage()
        {
            InitializeComponent();
            EventsDG.ItemsSource = App.DB.Event.ToList();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selected = EventsDG.SelectedValue as Event;
            if (selected != null)
            {
                NavigationService.Navigate(new TicketPage(selected.Hall, selected));
            }
        }
    }
}
