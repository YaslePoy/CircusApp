using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CircusApp.DB;

namespace CircusApp.Pages.Performance
{
    public partial class RepertPage : Page
    {
        private List<DB.Performance> _free, _owned;

        private Repertoire _performance;
        public RepertPage(Repertoire performance)
        {
            InitializeComponent();
            _performance = performance;
            var events = App.DB.Event.ToList();
            if(_performance != null)
                _owned = performance.PerformanceRepertoire.Select(i => i.Performance).ToList();
            else
                _owned = new List<DB.Performance>();
            var curEvent = App.DB.Event.FirstOrDefault(i => i.id == _performance.eventId);
            durCb.ItemsSource = events;
            if(curEvent != null)
                durCb.SelectedIndex = events.IndexOf(curEvent);
            CurrSpecLB.ItemsSource = _owned;
            var allSpecs = App.DB.Performance.ToList();
            _free = allSpecs.ToList().Where(i => !_owned.Any(x => x.id == i.id)).ToList();
            FreeSpecs.ItemsSource = _free;
            if (_performance.id == 0)
            {
                UsersGrid.Visibility = Visibility.Hidden;
            }           
            else
            {
                nameTb.Text = _performance.name;
                descTb.Text = _performance.description;
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (FreeSpecs.SelectedValue is null)
                return;
            var upSpec = FreeSpecs.SelectedValue as DB.Performance;
            _free.Remove(_free.FirstOrDefault(i => i.id == upSpec.id));
            _owned.Add(upSpec);
            CurrSpecLB.ItemsSource = null;
            FreeSpecs.ItemsSource = null;
            CurrSpecLB.ItemsSource = _owned;
            FreeSpecs.ItemsSource = _free;
            App.DB.PerformanceRepertoire.Add(new PerformanceRepertoire { performanceId = upSpec.id, repertoireId = _performance.id });
            App.DB.SaveChanges();        
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (CurrSpecLB.SelectedValue is null)
                return;
            var upSpec = CurrSpecLB.SelectedValue as DB.Performance;
            _owned.Remove(_owned.FirstOrDefault(i => i.id == upSpec.id));
            _free.Add(upSpec);
            CurrSpecLB.ItemsSource = null;
            FreeSpecs.ItemsSource = null;
            CurrSpecLB.ItemsSource = _owned;
            FreeSpecs.ItemsSource = _free;
            App.DB.PerformanceRepertoire.Remove(App.DB.PerformanceRepertoire.FirstOrDefault(i => i.performanceId == upSpec.id && i.repertoireId == _performance.id));
            App.DB.SaveChanges();        
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(nameTb.Text))
                return;
            if(string.IsNullOrWhiteSpace(descTb.Text))
                return;
            if(durCb.SelectedValue is null)
                return;

            if (_performance.id == 0)
            {
                _performance = new Repertoire();
                _performance.name = nameTb.Text;
                _performance.description = descTb.Text;
                _performance.eventId = (durCb.SelectedValue as DB.Event).id;
                App.DB.Repertoire.Add(_performance);
                App.DB.SaveChanges();
                UsersGrid.Visibility = Visibility.Visible;
            }
            else
            {
                _performance.name = nameTb.Text;
                _performance.description = descTb.Text;
                _performance.eventId = (durCb.SelectedValue as DB.Event).id;
                App.DB.SaveChanges();
            }
        }

    }
}