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

namespace ISIP324Kovalenko_WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>\
    
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (Core.CurrentUser != null)
            {
                MessageBox.Show("В аккаунт уже выполнен вход.", "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                return;
            }

            string login = UsernameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = Core.Context.User.FirstOrDefault(u => u.Login == login && u.Password == PasswordBox.Password);
            if (user == null)
            {
                MessageBox.Show("Неверное имя пользователя или пароль.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Core.CurrentUser = user;
            MessageBox.Show("Вы успешно вошли в систему!", "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
           Windows.RegisterWindow registerWindow = new Windows.RegisterWindow();
           registerWindow.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
