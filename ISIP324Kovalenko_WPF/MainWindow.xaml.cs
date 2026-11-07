using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ISIP324Kovalenko_WPF
{
    public partial class MainWindow : Window
    {
        public static string[] Models = { "Нищенская", "Менее крутая", "Крутая" };
        public static int[] ModelPrices = { 1100000, 1550000, 1750000 };

        public static string[] Engines = { "Не бензин", "Бензин разведённый", "Бензин 40000" };
        public static int[] EnginePrices = { 0, 50000, 100000 };

        public static string[] ColorsList = { "Белый", "Серебристый", "Серый", "Красный", "Чёрный" };
        public static int[] ColorPrices = { 0, 20000, 25000, 30000, 35000 };

        public static string[] OptionsList = { "Зимний пакет", "Камера заднего вида", "Мультимедийная система", "Доп. колесо" };
        public static int[] OptionPrices = { 35000, 22000, 45000, 18000 };

        public static int ModelIndex = 0;
        public static int EngineIndex = 0;
        public static int ColorIndex = 0;
        public static List<int> SelectedOptionIndices = new List<int>();

        public static int DownPaymentPercent = 20;
        public static int LoanTerm = 36;

        public static string ClientName = "";
        public static string ClientPhone = "";
        public static string ClientEmail = "";

        public static int BasePrice => ModelPrices[ModelIndex];
        public static int EnginePrice => EnginePrices[EngineIndex];
        public static int ColorPrice => ColorPrices[ColorIndex];
        public static int OptionsPrice
        {
            get
            {
                int sum = 0;
                foreach (int i in SelectedOptionIndices) sum += OptionPrices[i];
                return sum;
            }
        }
        public static int TotalPrice => BasePrice + EnginePrice + ColorPrice + OptionsPrice;
        public static int DownPayment => TotalPrice * DownPaymentPercent / 100;
        public static int LoanAmount => TotalPrice - DownPayment;
        public static int MonthlyPayment => LoanAmount / LoanTerm;

        private readonly string[] pages =
        {
            "pages/ModelAndEngine.xaml",
            "pages/Colors.xaml",
            "pages/Cost.xaml",
            "pages/Credit.xaml",
            "pages/Form.xaml"
        };
        private int step = 0;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Uri(pages[step], UriKind.Relative));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && step > 0)
            {
                step--;
                MainFrame.Navigate(new Uri(pages[step], UriKind.Relative));
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (step < 4)
                {
                    step++;
                    MainFrame.Navigate(new Uri(pages[step], UriKind.Relative));
                }
                else
                {
                    MessageBox.Show(
                        $"Заявка оформлена!\n\nКлиент: {ClientName}\nТелефон: {ClientPhone}\nEmail: {ClientEmail}\n" +
                        $"Стоимость: {TotalPrice} ₽\nПлатёж в месяц: {MonthlyPayment} ₽", "Готово");
                    button.Content = "Заявка оформлена";
                    button.IsEnabled = false;
                }
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            StepTitleTextBlock.Text = $"Шаг {step + 1} из 5";
            StepProgressBar.Value = step + 1;

            BackButton.Visibility = step == 0 ? Visibility.Collapsed : Visibility.Visible;
            NextButton.Content = step == 4 ? "Оформить заявку" : "Далее";
            NextButton.IsEnabled = step != 4 || (MainFrame.Content is pages.Form form && form.IsValid);

            RefreshConfigurationSummary();
        }

        public void RefreshConfigurationSummary()
        {
            TopTotalPriceTextBlock.Text = $"Текущая стоимость: {TotalPrice} ₽";
        }

        public void UpdateSubmitButton(bool isValid)
        {
            if (step == 4) NextButton.IsEnabled = isValid;
        }
    }
}