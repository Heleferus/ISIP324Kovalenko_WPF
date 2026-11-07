using System.Windows;
using System.Windows.Controls;

namespace ISIP324Kovalenko_WPF.pages
{
    public partial class ModelAndEngine : Page
    {
        public ModelAndEngine()
        {
            InitializeComponent();
            ModelComboBox.ItemsSource = MainWindow.Models;
            EngineComboBox.ItemsSource = MainWindow.Engines;

            ModelComboBox.SelectedIndex = MainWindow.ModelIndex;
            EngineComboBox.SelectedIndex = MainWindow.EngineIndex;
            UpdateTexts();
        }

        private void ModelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb != null && cb.SelectedIndex >= 0)
            {
                MainWindow.ModelIndex = cb.SelectedIndex;
                UpdateTexts();
            }
        }

        private void EngineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox cb && cb.SelectedIndex >= 0)
            {
                MainWindow.EngineIndex = cb.SelectedIndex;
                UpdateTexts();
            }
        }

        private void UpdateTexts()
        {
            if (EngineSurchargeTextBlock == null) return;

            BasePriceTextBlock.Text = $"Базовая стоимость: {MainWindow.BasePrice} ₽";
            EngineSurchargeTextBlock.Text = $"Доплата за двигатель: {MainWindow.EnginePrice} ₽";
            Step1SummaryTextBlock.Text = $"Итого за модель с выбранным двигателем: {MainWindow.BasePrice + MainWindow.EnginePrice} ₽";

            MainWindow win = Application.Current.MainWindow as MainWindow;
            if (win != null) win.RefreshConfigurationSummary();
        }
    }
}