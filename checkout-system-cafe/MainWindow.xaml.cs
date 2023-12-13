using System.Windows;
using System.Windows.Controls;
using System.Windows.Automation;

namespace checkout_system_cafe
{
    public partial class MainWindow : Window
    {
        private decimal _totalPriceAmount = 0; // Priset i SEK (kr)

        public MainWindow()
        {
            InitializeComponent();
            Coffeebutton();
        }

        public class Produkt
        {
            public string? Namn { get; set; }
            public decimal Pris { get; set; }
        }

        private static Produkt CreateCoffeeProduct()
        {
            return new Produkt
            {
                Namn = "Kaffe",
                Pris = 15
            };
        }

        private Button CreateCoffeeButton(Produkt product)
        {
            Button coffeeButton = new ()
            {
                Content = product.Namn,
                Width = 60,
                Height = 20,
                Margin = new Thickness(0, 0, 100, 480)
            };

            AutomationProperties.SetAutomationId(coffeeButton, "coffee");

            coffeeButton.Click += (sender, e) =>
            {
                _totalPriceAmount += product.Pris;
                UpdateDisplayedTotalPrice();
            };

            return coffeeButton;
        }

        private void Coffeebutton()
        {
            Produkt kaffe = CreateCoffeeProduct();
            Button coffeeButton = CreateCoffeeButton(kaffe);

            // Lägg till knappen till Grid i XAML
            mainGrid.Children.Add(coffeeButton);
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