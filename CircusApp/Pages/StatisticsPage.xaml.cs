using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace CircusApp.Pages
{
    public partial class StatisticsPage : Page
    {
        private List<EventStats> CollectStats()
        {
            List<EventStats> stats =
            App.DB.Ticket.ToList().GroupBy(i => i.Event).Select(i =>
            {
                var stat = new EventStats();
                stat.Name = i.Key.name;
                stat.Date = i.Key.timestamp;
                stat.Sold = i.Count();
                stat.Fill = stat.Sold / (double)(i.Key.Hall.columns * i.Key.Hall.rows) * 100;
                stat.Sum = i.Sum(x => (double)x.TicketClass.cost);
                return stat;
            }).ToList();
            return stats;
        }
        public StatisticsPage()
        {
            InitializeComponent();
            StatsDG.ItemsSource = CollectStats();
        }
    }

    public class EventStats
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Sold { get; set; }
        public double Fill { get; set; }
        public double Sum { get; set; }
    }
}