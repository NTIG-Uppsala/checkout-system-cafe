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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CoffeeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Du har valt kaffe!");
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("`du har genomför en återställning av priset!");
        }
    }
}