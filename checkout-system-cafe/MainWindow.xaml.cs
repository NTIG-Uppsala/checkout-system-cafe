using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace checkout_system_cafe
{
    public partial class MainWindow : Window
    {
        private decimal _totalPriceAmount = 0.00M; // Price in SEK (kr)
        const int BUTTON_WIDTH = 100;
        const int BUTTON_HEIGHT = 40;
        public ObservableCollection<Product>? Products = [];

        public MainWindow()
        {
            InitializeComponent();
            InitializeProductButtons();
            dataGrid.ItemsSource = Products;
        }

        public class Product
        {
            public string? Name { get; set; }

            public decimal Price { get; set; }
        }


        private void InitializeProductButtons()
        {
            List<Product> products = GetProducts();
            for (int i = 0; i < products.Count; i++)
            {
                Button productButton = CreateProductButton(products[i], i);
                mainGrid.Children.Add(productButton);
            }
        }

        private List<Product> GetProducts()
        {
            return
            [
                // Add products here
                new Product { Name = "Kaffe", Price = 15.00M},
                new Product { Name = "Cappuccino", Price = 30.00M},
                new Product { Name = "Bulle", Price = 12.50M},
                new Product { Name = "Te", Price = 15.00M},
                new Product { Name = "Iste", Price = 25.00M},
            ];
        }

        private Button CreateProductButton(Product product, int index)
        {
            Button productButton = new()
            {
                Content = product.Name,
                Width = BUTTON_WIDTH,
                Height = BUTTON_HEIGHT,
                Margin = new Thickness(0, index * (BUTTON_HEIGHT + 50), 250, 150),
            };

            AutomationProperties.SetAutomationId(productButton, product.Name?.ToLower());

            productButton.Click += (sender, e) =>
            {
                _totalPriceAmount += product.Price;
                UpdateDisplayedTotalPrice();
                Product newProduct = new() // Adds the chosen products to the order grid
                {
                    Name = product.Name,
                    Price = product.Price,
                };
                Products?.Add(newProduct);
            };
            return productButton;
        }

        private void UpdateDisplayedTotalPrice()
        {
            totalPrice.Content = $"{_totalPriceAmount} kr";
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            _totalPriceAmount = 0.00M;
            UpdateDisplayedTotalPrice();
            Products?.Clear();
        }

        private void PaymentClick(object sender, RoutedEventArgs e)
        {
            _totalPriceAmount = 0.00M;
            UpdateDisplayedTotalPrice();
            Products?.Clear();
        }
    }
}