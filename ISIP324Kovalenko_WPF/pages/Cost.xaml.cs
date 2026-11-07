using System.Collections.Generic;
using System.Windows.Controls;

namespace ISIP324Kovalenko_WPF.pages
{
    public partial class Cost : Page
    {
        public Cost()
        {
            InitializeComponent();

            SelectedModelTextBlock.Text = $"Модель: {MainWindow.Models[MainWindow.ModelIndex]}";
            SelectedEngineTextBlock.Text = $"Двигатель: {MainWindow.Engines[MainWindow.EngineIndex]}";
            SelectedColorTextBlock.Text = $"Цвет: {MainWindow.ColorsList[MainWindow.ColorIndex]}";

            List<string> chosen = new List<string>();
            foreach (int i in MainWindow.SelectedOptionIndices)
                chosen.Add(MainWindow.OptionsList[i]);

            SelectedOptionsListTextBlock.Text = chosen.Count == 0 ? "Не выбраны" : string.Join(", ", chosen);
            BreakdownBaseTextBlock.Text = $"Базовая стоимость: {MainWindow.BasePrice} ₽";
            BreakdownEngineTextBlock.Text = $"Двигатель: {MainWindow.EnginePrice} ₽";
            BreakdownColorTextBlock.Text = $"Цвет: {MainWindow.ColorPrice} ₽";
            BreakdownOptionsTextBlock.Text = $"Опции: {MainWindow.OptionsPrice} ₽";
            BreakdownTotalTextBlock.Text = $"Итого: {MainWindow.TotalPrice} ₽";
        }
    }
}