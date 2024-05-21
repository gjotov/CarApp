using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CarApp.Windows;
using CarsLibrary;
using LoggerLibrary;
namespace CarApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CarModel> _cars;
        private CarModel _selectedCar;
        private ObservableCollection<(string ServiceName, decimal ServiceCost)> _additionalServices;
        private decimal _totalPrice;

        public MainViewModel()
        {
            Cars = new ObservableCollection<CarModel>
            {
                new CarModel { Id = 1, Make = "Toyota", Model = "Camry", Price = 24000, HasDiscount = true, Discount = 10, ManufactureDate = new DateTime(2020, 1, 1), BodyType = BodyType.Sedan },
                new CarModel { Id = 2, Make = "Honda", Model = "Accord", Price = 22000, HasDiscount = false, ManufactureDate = new DateTime(2020, 1, 1), BodyType = BodyType.Sedan },
                new CarModel { Id = 3, Make = "Ford", Model = "Focus 3", Price = 25000, HasDiscount = false, ManufactureDate = new DateTime(2020, 1, 1), BodyType = BodyType.Sedan },
                new CarModel { Id = 4, Make = "Ford", Model = "Focus 2", Price = 20000, HasDiscount = false, ManufactureDate = new DateTime(2015, 1, 1), BodyType = BodyType.Sedan },
                new CarModel { Id = 5, Make = "Ford", Model = "Focus 1", Price = 15000, HasDiscount = false, ManufactureDate = new DateTime(2012, 1, 1), BodyType = BodyType.Sedan },
                new CarModel { Id = 6, Make = "Ford", Model = "Mustang", Price = 64000, HasDiscount = false, ManufactureDate = new DateTime(2024, 1, 1), BodyType = BodyType.Sedan },
            };

            AdditionalServices = new ObservableCollection<(string ServiceName, decimal ServiceCost)>
            {
                ("Гарантия", 1000),
                ("Страховка", 500),
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CarModel> Cars
        {
            get => _cars;
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }

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

        private ICommand _openConfigurationWindowCommand;
        public ICommand OpenConfigurationWindowCommand
        {
            get
            {
                if (_openConfigurationWindowCommand == null)
                {
                    _openConfigurationWindowCommand = new RelayCommand(
                        param => OpenConfigurationWindow(),
                        param => SelectedCar != null);
                }
                return _openConfigurationWindowCommand;
            }
        }

        private void OpenConfigurationWindow()
        {
            if (SelectedCar != null)
            {
                try
                {
                    Logger.LogInfo($"Открытие окна конфигурации для {SelectedCar.Make} {SelectedCar.Model}");
                    var configurationViewModel = new ConfigurationViewModel(SelectedCar, AdditionalServices ?? new ObservableCollection<(string ServiceName, decimal ServiceCost)>());
                    var configurationWindow = new CarConfigurationWindow
                    {
                        DataContext = configurationViewModel
                    };
                    configurationWindow.ShowDialog();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Ошибка при открытии окна конфигурации: {ex.Message}");
                }
            }
            else
            {
                Logger.LogWarning("Не выбран автомобиль для конфигурации.");
            }


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
