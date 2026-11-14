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

namespace ISIP324Kovalenko_WPF.Page
{
    /// <summary>
    /// Логика взаимодействия для Cart.xaml
    /// </summary>
    public partial class Cart : System.Windows.Controls.Page
    {
        public Cart()
        {
            InitializeComponent();
        }

        public static void AddToCart(Product product)
        {
            if (product == null)
                return;

            var cartItem = new CartItem
            {
                ProductId = product.Id,
                Quantity = 1,
                AddedAt = DateTime.Now
            };

            Core.Context.CartItem.Add(cartItem);
            Core.Context.SaveChanges();
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cartItem = button?.DataContext as CartItem;
            if (cartItem == null)
                return;
            Core.Context.CartItem.Remove(cartItem);
            Core.Context.SaveChanges();
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
