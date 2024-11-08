using System;
using System.Windows;
using System.Windows.Controls;

namespace CircusApp.Pages
{
    public partial class HelloPage : Page
    {
        public HelloPage()
        {
            InitializeComponent();
            timestampDP.SelectedDate = DateTime.Now;
            var x = 0;
            if (!int.TryParse(nameTB.Text, out x))
            {
                MessageBox.Show("Проверьте правильность введенных данных.");
                return;
            }
        }
    }
}