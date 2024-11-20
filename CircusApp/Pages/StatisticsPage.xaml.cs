using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using CircusApp.DB;

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
        
        private List<User> CollectArtStats()
        {
            List<User> stats =
                App.DB.User.Where(i => i.role_id == 2).ToList().Select(u =>
                {
                    if (u.UserPerformance.Count == 0)
                        return u;
                    var perfs = u.UserPerformance.Select(i => i.Performance).ToList();
                    if(perfs.Count == 0)
                        return u;
                    var reperts = perfs.Select(i => i.PerformanceRepertoire).ToList().Flat().Select(i => i.Repertoire).ToList();
                    if (reperts.Count == 0)
                        return u;
                    var events = reperts.Select(i => i.eventId).ToList();
                    if (events.Count == 0)
                        return u;
                    var tickets = App.DB.Ticket.ToList().Where(i => events.Contains(i.eventId));
                    if (tickets.Count() == 0)
                        return u;
                    u.Fill = tickets.GroupBy(i => i.Event).Select(i =>
                    {
                        var stat = new EventStats();
                        stat.Name = i.Key.name;
                        stat.Date = i.Key.timestamp;
                        stat.Sold = i.Count();
                        stat.Fill = stat.Sold / (double)(i.Key.Hall.columns * i.Key.Hall.rows) * 100;
                        stat.Sum = i.Sum(x => (double)x.TicketClass.cost);
                        return stat;
                    }).ToList().Average(i => i.Fill);
                    return u;
                }).ToList();
            return stats;
        }
        public StatisticsPage()
        {
            InitializeComponent();
            StatsDG.ItemsSource = CollectStats();
            ArtStatsDG.ItemsSource = CollectArtStats();
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