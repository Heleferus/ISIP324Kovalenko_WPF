using System.Windows;
using System.Windows.Controls;

namespace ISIP324Kovalenko_WPF.pages
{
    public partial class Credit : Page
    {
        public Credit()
        {
            InitializeComponent();
            DownPaymentSlider.Value = MainWindow.DownPaymentPercent;
            LoanTermSlider.Value = MainWindow.LoanTerm;
            UpdateTexts();
        }

        private void LoanSliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            if (slider != null)
            {
                if (slider == DownPaymentSlider)
                    MainWindow.DownPaymentPercent = (int)slider.Value;
                else if (slider == LoanTermSlider)
                    MainWindow.LoanTerm = (int)slider.Value;

                UpdateTexts();
            }
        }

        private void UpdateTexts()
        {
            if (MonthlyPaymentTextBlock == null) return;

            DownPaymentPercentLabel.Text = $"Первоначальный взнос: {MainWindow.DownPaymentPercent}%";
            LoanTermLabel.Text = $"Срок кредита: {MainWindow.LoanTerm} мес. (от 12 до 96 мес.)";

            CarTotalPriceTextBlock.Text = $"Стоимость автомобиля: {MainWindow.TotalPrice} ₽";
            DownPaymentAmountTextBlock.Text = $"Первоначальный взнос: {MainWindow.DownPayment} ₽";
            LoanAmountTextBlock.Text = $"Сумма кредита: {MainWindow.LoanAmount} ₽";
            MonthlyPaymentTextBlock.Text = $"Ежемесячный платёж: {MainWindow.MonthlyPayment} ₽";
        }
    }
}