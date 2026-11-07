using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ISIP324Kovalenko_WPF.pages
{
    public partial class Form : Page
    {
        public Form()
        {
            InitializeComponent();

            NameTextBox.Text = MainWindow.ClientName;
            PhoneTextBox.Text = MainWindow.ClientPhone;
            EmailTextBox.Text = MainWindow.ClientEmail;

            List<string> chosen = new List<string>();
            foreach (int i in MainWindow.SelectedOptionIndices)
                chosen.Add(MainWindow.OptionsList[i]);

            string options = chosen.Count == 0 ? "не выбраны" : string.Join(", ", chosen);

            FinalSummaryTextBlock.Text =
                $"Модель: {MainWindow.Models[MainWindow.ModelIndex]}\n" +
                $"Двигатель: {MainWindow.Engines[MainWindow.EngineIndex]}\n" +
                $"Цвет: {MainWindow.ColorsList[MainWindow.ColorIndex]}\n" +
                $"Опции: {options}\n" +
                $"Первоначальный взнос: {MainWindow.DownPaymentPercent}%\n" +
                $"Срок кредита: {MainWindow.LoanTerm} мес.\n" +
                $"Ежемесячный платёж: {MainWindow.MonthlyPayment} ₽";

            FinalPriceTextBlock.Text = $"Итого: {MainWindow.TotalPrice} ₽";
            CheckValid();
        }

        public bool IsValid =>
            MainWindow.ClientName.Trim().Length >= 2 &&
            MainWindow.ClientPhone.Trim().Length >= 10 &&
            MainWindow.ClientPhone.Trim().All(char.IsDigit) &&
            MainWindow.ClientEmail.Contains("@") && MainWindow.ClientEmail.Contains(".");

        private void ContactTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                if (tb == NameTextBox) MainWindow.ClientName = tb.Text;
                else if (tb == PhoneTextBox) MainWindow.ClientPhone = tb.Text;
                else if (tb == EmailTextBox) MainWindow.ClientEmail = tb.Text;

                CheckValid();
            }
        }

       

        private void CheckValid()
        {
            if (ValidationStatusTextBlock == null) return;

            ValidationStatusTextBlock.Text = IsValid? "Данные заполнены корректно." : "Заполните все поля корректно для активации кнопки заявки.";
            ValidationStatusTextBlock.Foreground = IsValid ? Brushes.SeaGreen : Brushes.Firebrick;

            MainWindow win = Application.Current.MainWindow as MainWindow;
            if (win != null) win.UpdateSubmitButton(IsValid);
        }

   
    }
}