using System.Windows;
using System.Windows.Controls;

namespace ISIP324Kovalenko_WPF.pages
{
    public partial class Colors : Page
    {
        public Colors()
        {
            InitializeComponent();
            ColorComboBox.ItemsSource = MainWindow.ColorsList;
            ColorComboBox.SelectedIndex = MainWindow.ColorIndex;

            for (int i = 0; i < MainWindow.OptionsList.Length; i++)
            {
                CheckBox cb = new CheckBox
                {
                    Content = $"{MainWindow.OptionsList[i]} — {MainWindow.OptionPrices[i]} ₽",
                    Tag = i,
                    Margin = new Thickness(0, 4, 0, 4),
                    IsChecked = MainWindow.SelectedOptionIndices.Contains(i)
                };
                cb.Checked += OptionCheckBox_Changed;
                cb.Unchecked += OptionCheckBox_Changed;
                OptionsStackPanel.Children.Add(cb);
            }

            UpdateTexts();
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb != null && cb.SelectedIndex >= 0)
            {
                MainWindow.ColorIndex = cb.SelectedIndex;
                UpdateTexts();
            }
        }

        private void OptionCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb)
            {
                int index = (int)cb.Tag;
                if (cb.IsChecked == true && !MainWindow.SelectedOptionIndices.Contains(index))
                    MainWindow.SelectedOptionIndices.Add(index);
                else
                    MainWindow.SelectedOptionIndices.Remove(index);

                UpdateTexts();
            }
        }

        private void UpdateTexts()
        {
            ColorPriceTextBlock.Text = $"Доплата за цвет: {MainWindow.ColorPrice} ₽";
            OptionsTotalTextBlock.Text = $"Стоимость выбранных опций: {MainWindow.OptionsPrice} ₽";

            MainWindow win = Application.Current.MainWindow as MainWindow;
            if (win != null) win.RefreshConfigurationSummary();
        }
    }
}