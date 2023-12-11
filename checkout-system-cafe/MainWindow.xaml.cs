using System.Windows;

namespace checkout_system_cafe
{
    public partial class MainWindow : Window
    {
#pragma warning disable IDE1006 // Naming Styles
        int totalPriceAmount = 0; // price in SEK (kr)
#pragma warning restore IDE1006 // Naming Styles
        public MainWindow()
        {
            InitializeComponent();
        }

        public void UpdateDisplayedTotalPrice()
        {
            totalPrice.Content = totalPriceAmount + " kr";
        }

        private void CoffeeClick(object sender, RoutedEventArgs e)
        {
            totalPriceAmount += 15;
            UpdateDisplayedTotalPrice();
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            totalPriceAmount = 0;
            UpdateDisplayedTotalPrice();
        }
    }
}