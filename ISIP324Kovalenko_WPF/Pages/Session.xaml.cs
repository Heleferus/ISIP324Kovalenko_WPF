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
    /// Логика взаимодействия для Session.xaml
    /// </summary>
    public partial class Session : Page
    {
        private ISIP324Kovalenko_WPF.Session currentsession;
        private int? selectedseat;
        public Session(ISIP324Kovalenko_WPF.Session session)
        {
            InitializeComponent();
            DataContext = session;
            currentsession = session;

            SessionInfoText.Text = $"{currentsession.Movie.Title} — Зал №{currentsession.Hall.HallNumber} ({currentsession.SessionDate:dd.MM.yyyy} {currentsession.SessionTime})";
            SessionPriceText.Text = $"Цена билета: {currentsession.Price} руб.";

            LoadSeats();
        }

        private void LoadSeats()
        {
            SeatsPanel.Children.Clear();
            List<int> bookedSeats = Core.Context.Ticket.Where(t => t.SessionId == currentsession.Id).Select(t => t.SeatNumber).ToList();
            bool hideOccupied = HideOccupiedCheckBox.IsChecked == true;
            int totalSeats = currentsession.Hall.SeatsCount;

            for (int seatnumber = 1; seatnumber <= totalSeats; seatnumber++)
            {
                bool IsOccupied = bookedSeats.Contains(seatnumber);
                if (hideOccupied && IsOccupied)
                {
                    continue; 
                }

                Button seatButton = new Button
                {
                    Content = seatnumber.ToString(),
                    Tag = seatnumber,
                    Width = 40,
                    Height = 40,
                    Margin = new Thickness(5),
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                };

                if(IsOccupied)
                {
                    seatButton.Background = Brushes.Red;
                    seatButton.IsEnabled = false;
                }
                else if (selectedseat == seatnumber)
                {
                    seatButton.Background = Brushes.Blue;
                    seatButton.Click += SeatButton_Click;
                }
                else
                {
                    seatButton.Background = Brushes.Green;
                    seatButton.Click += SeatButton_Click;
                }
                SeatsPanel.Children.Add(seatButton);
            }
        }

        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                int seatNumber = (int)clickedButton.Tag;
                selectedseat = seatNumber;
                SelectedSeatText.Text = $"Выбранное место: {seatNumber}";
                LoadSeats();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BuyTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedseat == null)
            {
                MessageBox.Show("Пожалуйста, выберите место.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new Ticket(currentsession, selectedseat.Value));
        }

        private void HideOccupiedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            LoadSeats();
        }
    }
}
