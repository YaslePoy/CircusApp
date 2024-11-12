using CircusApp.DB;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CircusApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        const double DegToRad = 0.0174532925;
        public TicketPage(Hall hall)
        {
            InitializeComponent();
            DrawSeats(hall);
        }

        private void DrawSeats(Hall hall)
        {
            const double SeatRate = 15f;
            var deg = 360d / hall.columns;
            var minDistance = hall.columns * (SeatRate + 3) / Math.PI / 2;
            var globalPlus = minDistance + hall.rows * (SeatRate + 3) + 10;
            for (int i = 0; i < hall.columns; i++)
            {
                var currentDeg = deg * i;
                var cos = Math.Cos(currentDeg * DegToRad);
                var sin = Math.Sin(currentDeg * DegToRad);

                for (int j = 0; j < hall.rows; j++)
                {
                    var distance = (SeatRate + 3) * j + minDistance;
                    var seat = new Ellipse { Height = SeatRate, Width = SeatRate, Fill = new SolidColorBrush(Colors.Gray) };
                    Canvas.SetLeft(seat, cos * distance + globalPlus);
                    Canvas.SetTop(seat, sin * distance + globalPlus);
                    SeatsCanvas.Children.Add(seat);
                }
            }
        }
    }
}
