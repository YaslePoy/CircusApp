using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CircusApp.DB;
using QRCoder;

namespace CircusApp.Pages
{
    /// <summary>
    ///     Логика взаимодействия для TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        private const double DegToRad = 0.0174532925;
        private int _row, _col;
        private Hall _hall;
        private Event _event;
        private List<Ticket> _ticketList;
        public TicketPage(Hall hall, Event ev)
        {

            InitializeComponent();
            _hall = hall;
            _event = ev;
            _ticketList = App.DB.Ticket.Where(i => i.eventId == ev.id).ToList();
            DrawSeats(hall);
            App.DB.User.Where(i => i.role_id == 5).ToList().ForEach(i => ViewerCB.Items.Add($"{i.surname} {i.name}"));
            ViewerCB.SelectionChanged += (s, e) =>
            {
                if (ViewerCB.SelectedValue is null)
                    return;
                var fullName = (ViewerCB.SelectedValue as string).Split(' ');
                SurnameTB.Text = fullName[0];
                NameTB.Text = fullName[1];
                UpdateQR();
            };
        }

        private void DrawSeats(Hall hall)
        {
            const double SeatRate = 15f;
            var deg = 360d / hall.columns;
            var minDistance = hall.columns * (SeatRate + 3) / Math.PI / 2;
            var globalPlus = minDistance + hall.rows * (SeatRate + 3) + 10;
            SeatsCanvas.Height = globalPlus * 2;
            SeatsCanvas.Width = globalPlus * 2;
            for (var i = 0; i < hall.columns; i++)
            {
                var currentDeg = deg * i;
                var cos = Math.Cos(currentDeg * DegToRad);
                var sin = Math.Sin(currentDeg * DegToRad);

                for (var j = 0; j < hall.rows; j++)
                {
                    var distance = (SeatRate + 3) * j + minDistance;
                    var seat = new Ellipse
                    { Height = SeatRate, Width = SeatRate, Fill = new SolidColorBrush(Colors.Black) };
                    if (!_ticketList.Any(x => x.column == i  && x.row == j))
                    {
                        seat.Fill = new SolidColorBrush(Colors.Gray);
                        seat.MouseEnter += (s, e) => { (s as Ellipse).Fill = new SolidColorBrush(Colors.Red); };
                        seat.MouseLeave += (s, e) => (s as Ellipse).Fill = new SolidColorBrush(Colors.Gray);
                        seat.MouseDown += (s, e) =>
                        {
                            var pos = seat.DataContext as (int, int)?;
                            _row = pos.Value.Item2;
                            _col = pos.Value.Item1;
                            ViewerSP.Visibility = Visibility.Visible;
                        };
                        seat.DataContext = (i, j);
                    }
                    else {
                        Console.WriteLine("test");
                    }
                    Canvas.SetLeft(seat, cos * distance + globalPlus);
                    Canvas.SetTop(seat, sin * distance + globalPlus);
                    SeatsCanvas.Children.Add(seat);
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var user = App.DB.User.FirstOrDefault(i => i.surname == SurnameTB.Text && i.name == NameTB.Text);
            if (user is null)
            {
                user = new User { surname = SurnameTB.Text, name = NameTB.Text, role_id = 5, login = string.Empty, password = string.Empty };
                App.DB.User.Add(user);
                App.DB.SaveChanges();
            }
            var classId = ClassCB.SelectedIndex + 1;
            var ticket = new Ticket { classId = classId, column = _col, row = _row, eventId = _event.id, userId = user.id };
            App.DB.Ticket.Add(ticket);
            App.DB.SaveChanges();
            SurnameTB.Text = string.Empty;
            NameTB.Text = string.Empty;
            ViewerCB.SelectedIndex = -1;
            ViewerSP.Visibility = Visibility.Collapsed;
            SeatsCanvas.Children.Clear();
            _ticketList.Add(ticket);
            DrawSeats(_hall);
        }

        private BitmapImage GenerateQrCodeBitmapImage(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    using (Bitmap qrBitmap = qrCode.GetGraphic(20))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            qrBitmap.Save(ms, ImageFormat.Png);
                            ms.Position = 0;
                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.StreamSource = ms;
                            bitmapImage.EndInit();
                            return bitmapImage;
                        }
                    }
                }
            }
        }

        private void SurnameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateQR();
        }

        private void ClassCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CostTB is null) return;
            UpdateQR();
            switch (ClassCB.SelectedIndex)
            {
                case 0:
                    CostTB.Text = "500";
                    break;
                case 1:
                    CostTB.Text = "800";
                    break;
                case 2:
                    CostTB.Text = "1400";
                    break;
            }
        }

        private void UpdateQR()
        {
            if (QRImage != null)
                QRImage.Source = GenerateQrCodeBitmapImage($"{NameTB.Text};{SurnameTB.Text};{_row};{_col};{ClassCB.SelectedItem};{_hall.id}");
        }
    }
}