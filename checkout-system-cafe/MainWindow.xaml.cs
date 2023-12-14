using System.Windows;
using System.Windows.Controls;
using System.Windows.Automation;
namespace checkout_system_cafe
{
    public partial class MainWindow : Window
    {
        private decimal _totalPriceAmount = 0; // Price in SEK (kr)
        const int BUTTON_WIDTH = 100;
        const int BUTTON_HEIGHT = 40;

        public MainWindow()
        {
            InitializeComponent();
            InitializeProductButtons();
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
                new Product { Name = "Kaffe", Price = 15 },
                new Product { Name = "Cappuccino", Price = 30},
                new Product { Name = "Bulle", Price = 10 },
                new Product { Name = "Te", Price = 15 },
                new Product { Name = "Iste", Price = 25 }
            ];
        }

        private Button CreateProductButton(Product product, int index)
        {
            Button productButton = new()
            {
                Content = product.Name,
                Width = BUTTON_WIDTH,
                Height = BUTTON_HEIGHT,
                Margin = new Thickness(0, index * (BUTTON_HEIGHT + 50), 250, 150)
            };

            AutomationProperties.SetAutomationId(productButton, product.Name?.ToLower());

            productButton.Click += (sender, e) =>
            {
                _totalPriceAmount += product.Price;
                UpdateDisplayedTotalPrice();
            };

            return productButton;
        }

        private void UpdateDisplayedTotalPrice()
        {
            totalPrice.Content = $"{_totalPriceAmount} kr";
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            _totalPriceAmount = 0;
            UpdateDisplayedTotalPrice();
        }
    }
}