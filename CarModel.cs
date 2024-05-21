using CarsLibrary;
using System;

public class CarModel
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public bool HasDiscount { get; set; }
    public decimal Discount { get; set; }
    public DateTime ManufactureDate { get; set; }
    public BodyType BodyType { get; set; }
    public string Configuration { get; set; }
}
