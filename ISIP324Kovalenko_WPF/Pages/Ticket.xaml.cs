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

namespace ISIP324Kovalenko_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Ticket.xaml
    /// </summary>
    public partial class Ticket : Page
    {
        private ISIP324Kovalenko_WPF.Session currentsession;
        private int selectedseat;
        public Ticket(ISIP324Kovalenko_WPF.Session session, int seatNumber)
        {
            InitializeComponent();
            DataContext = session;
            currentsession = session;

            TicketInfoText.Text = $"{currentsession.Movie.Title} — Зал №{currentsession.Hall.HallNumber} ({currentsession.SessionDate:dd.MM.yyyy} {currentsession.SessionTime})\nМесто: {seatNumber}";
            TicketPriceText.Text = $"Цена: {currentsession.Price}";
        }

        private void RegisterTicketButton_Click(object sender, RoutedEventArgs e)
        {
            var ticket = new ISIP324Kovalenko_WPF.Ticket
            {
                SessionId = currentsession.Id,
                SeatNumber = selectedseat,
                UserId = Core.CurrentUser.Id,
            };
            Core.Context.Ticket.Add(ticket);
            Core.Context.SaveChanges();
            MessageBox.Show("Билет успешно оформлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new MainPage());
        }
    }
}
