using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;

namespace Checkout_system_cafe
{
    public partial class MainWindow : Window
    {
        private decimal _totalPriceAmount = 0.00M; // Price in SEK (kr)
        const int BUTTON_WIDTH = 100;
        const int BUTTON_HEIGHT = 40;
        public ObservableCollection<Product> Products = [];
        public ObservableCollection<Product> SavedProducts { get; set; } = []; // Product history list

        public MainWindow()
        {
            InitializeComponent();
            InitializeProductButtons();
            dataGrid.ItemsSource = Products;
            historyDataGrid.ItemsSource = SavedProducts;
        }

        public class Product : INotifyPropertyChanged
        {
            public string? Name { get; set; }

            private decimal _price;
            public decimal Price
            {
                get { return _price; }

                // If price is changed, xaml/user interface is notified and updated
                set
                {
                    if (_price != value)
                    {
                        _price = value;
                        OnPropertyChanged(nameof(Price)); // The event which notifies xaml/user interface with name of the changed property
                    }
                }
            }

            private int _amount;
            public int Amount
            {
                get { return _amount; }
                set
                {
                    if (_amount != value)
                    {
                        _amount = value;
                        OnPropertyChanged(nameof(Amount));
                    }
                }
            }

            // Implements the INotifyPropertyChanged interface to notify other parts of the program when a property changes
            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                // Triggers the PropertyChanged event which notifies xaml code/user interface
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
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
                new Product { Name = "Kaffe", Price = 15.00M },
                new Product { Name = "Cappuccino", Price = 30.00M },
                new Product { Name = "Bulle", Price = 12.50M },
                new Product { Name = "Te", Price = 15.00M },
                new Product { Name = "Iste", Price = 25.00M }
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
                var existingProduct = Products?.FirstOrDefault(item => item.Name == product.Name); // Checks if chosen product is already in product grid

                if (existingProduct != null)
                {
                    existingProduct.Amount++;
                    existingProduct.Price += product.Price;
                    _totalPriceAmount += product.Price;
                }
                else
                {
                    Product newProduct = new()
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Amount = 1
                    };
                    Products?.Add(newProduct);
                    _totalPriceAmount += product.Price;
                }
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
            _totalPriceAmount = 0.00M;
            UpdateDisplayedTotalPrice();
            Products?.Clear();
        }

        private void PaymentClick(object sender, RoutedEventArgs e)
        {
            // If "betala" button is pressed, products in the original grid is copied to the history grid
            foreach (var product in Products)
            {
                SavedProducts.Add(new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    Amount = product.Amount
                });
            }
            _totalPriceAmount = 0.00M;
            UpdateDisplayedTotalPrice();
            Products?.Clear();
        }

        private void ShowHistoryClick(object sender, RoutedEventArgs e)
        {
            historyDataGrid.Visibility = Visibility.Visible;
        }
        private void HideHistoryClick(object sender, RoutedEventArgs e)
        {
            historyDataGrid.Visibility = Visibility.Collapsed;
        }
    }
}