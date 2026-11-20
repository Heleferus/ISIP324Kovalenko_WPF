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
using System.Windows.Shapes;

namespace ISIP324Kovalenko_WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string fullName = FullNameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните логин, ФИО и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (login.Length > 50 || password.Length > 50 || fullName.Length > 100)
            {
                MessageBox.Show("Максимальная длина: логин и пароль — 50 символов, ФИО — 100 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var existingUser = Core.Context.User.FirstOrDefault(user => user.Login == login);
            if (existingUser != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User newUser = new User
            {
                Login = login,
                FullName = fullName,
                Password = password
            };
            Core.Context.User.Add(newUser);
            Core.Context.SaveChanges();
            MessageBox.Show("Регистрация прошла успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
