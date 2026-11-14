using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : System.Windows.Controls.Page
    {
        public Order()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            lstOrders.ItemsSource = Core.Context.Order.OrderByDescending(o => o.CreatedAt).ToList();
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Заполните имя, фамилию, email и адрес.");
                return;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Введите корректный email.");
                return;
            }
            var cartItems = Core.Context.CartItem.Include(c => c.Product).ToList();
            if (!cartItems.Any())
            {
                MessageBox.Show("Корзина пуста.");
                return;
            }

            var totalPrice = cartItems.Sum(c => (c.Product?.Price ?? 0m) * c.Quantity);

            var order = new ISIP324Kovalenko_WPF.Order
            {
                Name = txtName.Text.Trim(),
                Surname = txtLastName.Text.Trim(),
                DeliveryAddress = txtAddress.Text.Trim(),
                CreatedAt = DateTime.Now,
                TotalPrice = totalPrice,
                Email = txtEmail.Text.Trim()
            };

            Core.Context.Order.Add(order);
            Core.Context.SaveChanges();

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product != null ? item.Product.Price : 0m
                };

                Core.Context.OrderItem.Add(orderItem);
            }

            Core.Context.CartItem.RemoveRange(cartItems);
            Core.Context.SaveChanges();

            txtName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            LoadOrders();
            MessageBox.Show("Заказ оформлен.");
        }
    }
}
