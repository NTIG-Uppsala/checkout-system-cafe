using System.Windows;

namespace checkout_system_cafe
{
    public partial class MainWindow : Window
    {
        int totalPriceAmount = 0; // price in SEK (kr)
        public MainWindow()
        {
            InitializeComponent();
        }

        public void UpdateDisplayedTotalPrice()
        {
            totalPrice.Content = totalPriceAmount + " kr";
        }

        private void Coffee_Click(object sender, RoutedEventArgs e)
        {
            totalPriceAmount += 15;
            UpdateDisplayedTotalPrice();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            totalPriceAmount = 0;
            UpdateDisplayedTotalPrice();
        }
    }
}