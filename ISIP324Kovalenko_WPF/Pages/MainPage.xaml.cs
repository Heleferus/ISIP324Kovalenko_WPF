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

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

       private void LoginButton_Click(object sender, RoutedEventArgs e)
       {
            Windows.LoginWindow loginWindow = new Windows.LoginWindow();
            loginWindow.ShowDialog();
       }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
          
        }

    }
}
