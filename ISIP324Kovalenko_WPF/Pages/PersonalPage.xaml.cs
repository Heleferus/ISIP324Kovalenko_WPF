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

namespace ISIP324Kovalenko_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для PersonalPage.xaml
    /// </summary>
    public partial class PersonalPage : Page
    {
        public PersonalPage()
        {
            InitializeComponent();

            if (Core.CurrentUser == null)return;

            DataContext = Core.CurrentUser;

            PurchasedTickets.ItemsSource = Core.Context.Ticket.Where(ticket => ticket.UserId == Core.CurrentUser.Id).OrderByDescending(ticket => ticket.Id).ToList();
        }
    }
}
