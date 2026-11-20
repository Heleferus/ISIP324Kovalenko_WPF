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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
        public MainPage()
        {
            InitializeComponent();
            movies = new ObservableCollection<Movie>(Core.Context.Movie.ToList());
            lstMovies.ItemsSource = movies;
            SortComboBox.SelectedIndex = 0;
            UpdateAuthButton();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                movies = new ObservableCollection<Movie>(Core.Context.Movie.ToList());
            }
            else
            {
                string searchText = SearchTextBox.Text.ToLower();
                var filteredMovies = Core.Context.Movie.Where(movie => movie.Title.ToLower().Contains(searchText)).ToList();
                movies = new ObservableCollection<Movie>(filteredMovies);
            }
            lstMovies.ItemsSource = movies;
        }

       private void LoginButton_Click(object sender, RoutedEventArgs e)
       {
           if (Core.CurrentUser != null)
           {
               Core.CurrentUser = null;
               UpdateAuthButton();
               MessageBox.Show("Вы вышли из аккаунта.", "Выход", MessageBoxButton.OK, MessageBoxImage.Information);
               return;
           }

           Windows.LoginWindow loginWindow = new Windows.LoginWindow
           {
               Owner = Window.GetWindow(this)
           };
           loginWindow.ShowDialog();
           UpdateAuthButton();
       }

       private void UpdateAuthButton()
       {
           AuthButton.Content = Core.CurrentUser == null ? "Войти" : "Выйти из аккаунта";
       }

       private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {
           if (SortComboBox.SelectedItem == null)
                return;

            string sortBy = (SortComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString();
            List<Movie> sortedMovies;

            if (sortBy == "Rating")
                sortedMovies = movies.OrderByDescending(movie => movie.Rating).ToList();
            else
                sortedMovies = movies.OrderBy(movie => movie.Title).ToList();

            movies = new ObservableCollection<Movie>(sortedMovies);
            lstMovies.ItemsSource = movies;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
