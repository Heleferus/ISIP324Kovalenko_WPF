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
        }
    }
}
