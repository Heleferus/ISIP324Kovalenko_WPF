using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
        ObservableCollection<CartItem> items { get; set; }
        public Cart()
        {
            InitializeComponent();

            items = new ObservableCollection<CartItem>(Core.Context.CartItem.Include(c => c.Product).ToList());
            lstCart.ItemsSource = items;
            this.Loaded += Cart_Loaded;
        }


        private void Cart_Loaded(object sender, RoutedEventArgs e)
        {
            lstCart.Items.Refresh();
        }

        public static void AddToCart(Product product)
        {
            if (product == null)
                return;

            var existingItem = Core.Context.CartItem.FirstOrDefault(c => c.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
                existingItem.AddedAt = DateTime.Now;
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = product.Id,
                    Quantity = 1,
                    AddedAt = DateTime.Now
                };

                Core.Context.CartItem.Add(cartItem);
            }

            Core.Context.SaveChanges();
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cartItem = button?.DataContext as CartItem;
            if (cartItem == null)
                return;

            var trackedItem = Core.Context.CartItem.Find(cartItem.Id);
            if (trackedItem != null)
            {
                Core.Context.CartItem.Remove(trackedItem);
                Core.Context.SaveChanges();
            }

            items.Remove(cartItem);
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Page.Order());
        }
    }
}
