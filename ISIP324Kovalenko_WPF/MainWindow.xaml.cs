using ISIP324Kovalenko_WPF.pages;
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

namespace ISIP324Kovalenko_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Uri("pages/ModelAndEngine.xaml", UriKind.Relative));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is pages.ModelAndEngine)
            {
                MainFrame.Navigate(new Uri("pages/Colors.xaml", UriKind.Relative));
            }
            else if (MainFrame.Content is pages.Colors)
            {
                MainFrame.Navigate(new Uri("pages/Cost.xaml", UriKind.Relative));
            }
            else if (MainFrame.Content is pages.Cost)
            {
                MainFrame.Navigate(new Uri("pages/Credit.xaml", UriKind.Relative));
            }
            else if (MainFrame.Content is pages.Credit)
            {
                MainFrame.Navigate(new Uri("pages/Form.xaml", UriKind.Relative));
            }
            else if (MainFrame.Content is pages.Form form)
            {
                string name = form.NameTextBox.Text.Trim();
                string phone = form.PhoneTextBox.Text.Trim();
                string email = form.EmailTextBox.Text.Trim();
                int atIndex = email.IndexOf('@');

                if (name.Length < 2 || phone.Length < 10 || phone.Length > 15 ||
                    !phone.All(char.IsDigit) || atIndex <= 0 || email.IndexOf('.', atIndex) < 0)
                {
                    MessageBox.Show("Проверьте имя, телефон и адрес электронной почты.",
                        "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBox.Show("Заявка оформлена!", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            int currentStep = 0;

            if (MainFrame.Content is pages.ModelAndEngine)
            {
                currentStep = 1;
            }
            else if (MainFrame.Content is pages.Colors)
            {
                currentStep = 2;
            }
            else if (MainFrame.Content is pages.Cost)
            {
                currentStep = 3;
            }
            else if (MainFrame.Content is pages.Credit)
            {
                currentStep = 4;
            }
            else if (MainFrame.Content is pages.Form)
            {
                currentStep = 5;
            }

            if (currentStep > 0)
            {
                StepTitleTextBlock.Text = $"Шаг {currentStep} из 5";
                StepProgressBar.Value = currentStep;
            }

            BackButton.Visibility = currentStep == 1 ? Visibility.Collapsed : Visibility.Visible;
            BackButton.IsEnabled = currentStep > 1 && MainFrame.CanGoBack;
            NextButton.Content = currentStep == 5 ? "Оформить заявку" : "Далее";
            NextButton.IsEnabled = currentStep > 0;
        }

    }
}
