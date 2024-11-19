using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CircusApp.DB;

namespace CircusApp.Pages.Performance
{
    public partial class PerformancePage : Page
    {
        private List<User> _free, _owned;

        private DB.Performance _performance;
        public PerformancePage(DB.Performance performance)
        {
            InitializeComponent();
            _performance = performance;
            if(_performance != null) 
                _owned = performance.UserPerformance.Select(i => i.User1).ToList();
            else
                _owned = new List<User>();
            CurrSpecLB.ItemsSource = _owned;
            var allSpecs = App.DB.User.Where(i => i.role_id == 2).ToList();
            _free = allSpecs.ToList().Where(i => !_owned.Any(x => x.id == i.id)).ToList();
            FreeSpecs.ItemsSource = _free;
            if (_performance is null)
            {
                UsersGrid.IsEnabled = false;
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (FreeSpecs.SelectedValue is null)
                return;
            var upSpec = FreeSpecs.SelectedValue as User;
            _free.Remove(_free.FirstOrDefault(i => i.id == upSpec.id));
            _owned.Add(upSpec);
            CurrSpecLB.ItemsSource = null;
            FreeSpecs.ItemsSource = null;
            CurrSpecLB.ItemsSource = _owned;
            FreeSpecs.ItemsSource = _free;
            App.DB.UserPerformance.Add(new UserPerformance { user = upSpec.id, performanceId = _performance.id });
            App.DB.SaveChanges();        
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (CurrSpecLB.SelectedValue is null)
                return;
            var upSpec = CurrSpecLB.SelectedValue as User;
            _owned.Remove(_owned.FirstOrDefault(i => i.id == upSpec.id));
            _free.Add(upSpec);
            CurrSpecLB.ItemsSource = null;
            FreeSpecs.ItemsSource = null;
            CurrSpecLB.ItemsSource = _owned;
            FreeSpecs.ItemsSource = _free;
            App.DB.UserPerformance.Remove(App.DB.UserPerformance.FirstOrDefault(i => i.user == upSpec.id && i.performanceId == _performance.id));
            App.DB.SaveChanges();        
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(nameTb.Text))
                return;
            if(string.IsNullOrWhiteSpace(descTb.Text))
                return;
            if(string.IsNullOrWhiteSpace(durTb.Text))
                return;
            
            if(!int.TryParse(durTb.Text, out int dur))
                return;

            if (_performance is null)
            {
                _performance = new DB.Performance();
                _performance.name = nameTb.Text;
                _performance.description = descTb.Text;
                _performance.duration = TimeSpan.FromMinutes(dur);
                App.DB.Performance.Add(_performance);
                App.DB.SaveChanges();
                UsersGrid.IsEnabled = true;
            }
            else
            {
                _performance.name = nameTb.Text;
                _performance.description = descTb.Text;
                _performance.duration = TimeSpan.FromMinutes(dur);
                App.DB.SaveChanges();
            }
        }
    }
}