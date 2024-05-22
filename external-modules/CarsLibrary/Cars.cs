using System;
using System.Collections.Generic;
using System.Text;


namespace CarsLibrary
{
    public enum BodyType
    {
        Phaeton,
        Limousine,
        Cabriolet,
        Sedan,
        Roadster,
        Coupe,
        Pickup,
        Wagon,
        Hatchback
    }
    /// <summary>
    /// Класс параметров машин.
    /// </summary>
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public bool HasDiscount { get; set; }
        public decimal Discount { get; set; }
        public DateTime ManufactureDate { get; set; }
        public BodyType BodyType { get; set; }

        public decimal GetDiscountedPrice()
        {
            if (HasDiscount)
            {
                return Price - (Price * Discount / 100);
            }
            return Price;
        }

        public decimal CalculateDiscount(decimal finalPrice)
        {
            if (Price > 0)
            {
                return ((Price - finalPrice) / Price) * 100;
            }
            return 0;
        }
    }
    /// <summary>
    /// Класс комплектации машины.
    /// </summary>
    public class CarConfiguration
    {
        public string Engine { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }
        public List<string> AdditionalFeatures { get; set; }

        public override string ToString()
        {
            return $"Engine: {Engine}, Transmission: {Transmission}, Color: {Color}, Additional Features: {string.Join(", ", AdditionalFeatures)}";
        }
    }

    /// <summary>
    /// Паттерн строитель реализующий класс комплектации машины.
    /// </summary>
    public class CarBuilder
    {
        private CarConfiguration _configuration = new CarConfiguration();

        public CarBuilder SetEngine(string engine)
        {
            _configuration.Engine = engine;
            return this;
        }

        public CarBuilder SetTransmission(string transmission)
        {
            _configuration.Transmission = transmission;
            return this;
        }

        public CarBuilder SetColor(string color)
        {
            _configuration.Color = color;
            return this;
        }

        public CarBuilder AddFeature(string feature)
        {
            if (_configuration.AdditionalFeatures == null)
            {
                _configuration.AdditionalFeatures = new List<string>();
            }
            _configuration.AdditionalFeatures.Add(feature);
            return this;
        }

        public CarConfiguration Build()
        {
            return _configuration;
        }
    }
    /// <summary>
    /// Методы для расчета скидки и стоимости.
    /// </summary>
    public class PurchaseReceipt
    {
        public string CarDealerName { get; set; }
        public string CarName { get; set; }
        public CarConfiguration Configuration { get; set; }
        public List<(string ServiceName, decimal ServiceCost)> AdditionalServices { get; set; }
        public decimal TotalPrice { get; set; }

        public string GenerateReceipt()
        {
            var receipt = new StringBuilder();
            receipt.AppendLine($"Дилер: {CarDealerName}");
            receipt.AppendLine($"Машина: {CarName}");
            receipt.AppendLine($"Комплектация: {Configuration}");
            receipt.AppendLine("Дополнительные услуги:");

            foreach (var service in AdditionalServices)
            {
                receipt.AppendLine($"{service.ServiceName}: {service.ServiceCost:C}");
            }

            receipt.AppendLine($"Итоговая стоимость: {TotalPrice:C}");
            return receipt.ToString();
        }
    }

}
