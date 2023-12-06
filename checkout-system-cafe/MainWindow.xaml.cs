using System.Windows;

namespace checkout_system_cafe
{
    public partial class MainWindow : Window
    {
        int value = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void UpdateDisplayedTotalPrice()
        {
            totalPrice.Content = value + " kr";
        }

        private void Coffee_Click(object sender, RoutedEventArgs e)
        {
            value += 15;
            UpdateDisplayedTotalPrice();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            value = 0;
            UpdateDisplayedTotalPrice();
        }
    }
}