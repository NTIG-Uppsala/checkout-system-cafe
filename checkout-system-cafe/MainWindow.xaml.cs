using System.Windows;
using System.Windows.Controls;
using System.Windows.Automation;

namespace checkout_system_cafe
{
    public partial class MainWindow : Window
    {
        private decimal totalPriceAmount = 0; // Priset i SEK (kr)

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

        private void Coffeebutton()
        {
            Produkt kaffe = new()
            {
                Namn = "Kaffe",
                Pris = 15
            };

            Button coffeebutton = new()
            {
                Content = kaffe.Namn,
                Width = 60,
                Height = 20,
                Margin = new Thickness(0, 0, 100, 480)
            };

            coffeebutton.Click += (sender, e) =>
            AutomationProperties.SetAutomationId(coffeeButton, "coffee");
            {
                totalPriceAmount += kaffe.Pris;
                UpdateDisplayedTotalPrice();
            };

            // Lägg till knappen till Grid i XAML
            mainGrid.Children.Add(coffeebutton);
        }

        private void UpdateDisplayedTotalPrice()
        {
            totalPrice.Content = $"{totalPriceAmount} kr";
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            totalPriceAmount = 0;
            UpdateDisplayedTotalPrice();
        }
    }
}