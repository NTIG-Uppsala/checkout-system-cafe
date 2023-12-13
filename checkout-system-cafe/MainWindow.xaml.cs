using System.Windows;
using System.Windows.Controls;
using System.Windows.Automation;

namespace checkout_system_cafe
{
    public partial class MainWindow : Window
    {
        private decimal _totalPriceAmount = 0; // Price in SEK (kr)

        public MainWindow()
        {
            InitializeComponent();
            Coffeebutton();
        }

        public class Product
        {
            public string? Name { get; set; }
            public decimal Price { get; set; }
        }

        private static Product CreateCoffeeProduct()
        {
            return new Product
            {
                Name = "Kaffe",
                Price = 15
            };
        }

        private Button CreateCoffeeButton(Product product)
        {
            Button coffeeButton = new ()
            {
                Content = product.Name,
                Width = 60,
                Height = 20,
                Margin = new Thickness(0, 0, 100, 480)
            };

            AutomationProperties.SetAutomationId(coffeeButton, "coffee");

            coffeeButton.Click += (sender, e) =>
            {
                _totalPriceAmount += product.Price;
                UpdateDisplayedTotalPrice();
            };

            return coffeeButton;
        }

        private void Coffeebutton()
        {
            Product kaffe = CreateCoffeeProduct();
            Button coffeeButton = CreateCoffeeButton(kaffe);

            mainGrid.Children.Add(coffeeButton); // Add button to Grid in XAML
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