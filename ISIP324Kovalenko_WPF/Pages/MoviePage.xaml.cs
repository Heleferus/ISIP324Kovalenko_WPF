using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для MoviePage.xaml
    /// </summary>
    public partial class MoviePage : Page
    {
        public MoviePage() : this(null)
        {
        }

        public MoviePage(Movie movie)
        {

            InitializeComponent();
            DataContext = movie;
            var movieSession = Core.Context.Session.Where(s => s.MovieId == movie.Id).OrderBy(s => s.SessionTime).ThenBy(s => s.SessionTime).ToList();
            lstSessions.ItemsSource = movieSession;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
           var button = sender as Button;
           var selectedSession = button?.DataContext as ISIP324Kovalenko_WPF.Session;
           if (selectedSession != null)
            {
                int bookedSeatsCount = Core.Context.Ticket.Count(b => b.SessionId == selectedSession.Id);
                int totalSeats = selectedSession.Hall.SeatsCount;
                if (bookedSeatsCount >= totalSeats)
                {
                    MessageBox.Show("Все места на эту сессию уже забронированы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Core.CurrentUser == null)
                {
                    MessageBox.Show("Пожалуйста, войдите в систему, чтобы забронировать билет.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                NavigationService.Navigate(new Session(selectedSession));
            }

        }
    }
}
