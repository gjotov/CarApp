using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CarsLibrary;

namespace CarApp
{
    public partial class ConfigurationViewModel : INotifyPropertyChanged
    {
        public CarModel _selectedCar;
        public ObservableCollection<(string ServiceName, decimal ServiceCost)> _additionalServices;
        private decimal _totalPrice;

        public ConfigurationViewModel(CarModel selectedCar, ObservableCollection<(string ServiceName, decimal ServiceCost)> additionalServices)
        {
            SelectedCar = selectedCar;
            AdditionalServices = additionalServices;
            CalculateTotalPrice();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CarModel SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                OnPropertyChanged();
                CalculateTotalPrice();
            }
        }

        public ObservableCollection<(string ServiceName, decimal ServiceCost)> AdditionalServices
        {
            get => _additionalServices;
            set
            {
                _additionalServices = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                OnPropertyChanged();
            }
        }

        private ICommand _generateReceiptCommand;
        public ICommand GenerateReceiptCommand
        {
            get
            {
                if (_generateReceiptCommand == null)
                {
                    _generateReceiptCommand = new RelayCommand(
                        param => GenerateReceipt(),
                        param => SelectedCar != null);
                }
                return _generateReceiptCommand;
            }
        }

        private void GenerateReceipt()
        {
            var receipt = new PurchaseReceipt
            {
                CarDealerName = "Клевые тачки",
                CarName = $"{SelectedCar.Make} {SelectedCar.Model}",
                Configuration = new CarBuilder()
                                .SetEngine("V6")
                                .SetTransmission("Автомат")
                                .SetColor("Красный")
                                .AddFeature("Кожаный салон")
                                .AddFeature("Люк")
                                .Build(),
                AdditionalServices = AdditionalServices.ToList(),
                TotalPrice = TotalPrice
            };

            MessageBox.Show(receipt.GenerateReceipt(), "Оформить");
        }

        private void CalculateTotalPrice()
        {
            if (SelectedCar != null)
            {
                var discountedPrice = SelectedCar.Price;
                if (SelectedCar.HasDiscount)
                {
                    discountedPrice -= (SelectedCar.Price * SelectedCar.Discount / 100);
                }

                TotalPrice = discountedPrice + (AdditionalServices?.Sum(s => s.ServiceCost) ?? 0);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
