using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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