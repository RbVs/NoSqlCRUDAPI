using Microsoft.AspNetCore.Mvc;
using NoSqlCRUDAPI.Database;
using NoSqlCRUDAPI.Models;

namespace NoSqlCRUDAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedDatabaseController : ControllerBase
{
    private readonly Repository<Car> _carRepository;
    private readonly Repository<Manufacturer> _manufacturerRepository;

    public SeedDatabaseController(Repository<Manufacturer> manufacturerRepository, Repository<Car> carRepository)
    {
        _manufacturerRepository = manufacturerRepository;
        _carRepository = carRepository;
    }

    [HttpPost]
    public IActionResult Seed()
    {
        // Define known German manufacturers and their cars
        var germanManufacturers = new List<Manufacturer>
        {
            new()
            {
                Name = "BMW",
                Country = "Germany",
                Address = "Munich, Germany",
                Cars = new List<Car>
                {
                    new() { Model = "BMW 3 Series", Year = 2021, Color = "Black", Price = 41000m },
                    new() { Model = "BMW X5", Year = 2020, Color = "White", Price = 61000m }
                }
            },
            new()
            {
                Name = "Mercedes-Benz",
                Country = "Germany",
                Address = "Stuttgart, Germany",
                Cars = new List<Car>
                {
                    new() { Model = "Mercedes-Benz C-Class", Year = 2021, Color = "Silver", Price = 43000m },
                    new() { Model = "Mercedes-Benz GLE", Year = 2019, Color = "Black", Price = 55000m }
                }
            },
            new()
            {
                Name = "Volkswagen",
                Country = "Germany",
                Address = "Wolfsburg, Germany",
                Cars = new List<Car>
                {
                    new() { Model = "Volkswagen Golf", Year = 2020, Color = "Blue", Price = 25000m },
                    new() { Model = "Volkswagen Passat", Year = 2019, Color = "Gray", Price = 28000m }
                }
            }
        };

        // Define known US manufacturers and their cars
        var usManufacturers = new List<Manufacturer>
        {
            new()
            {
                Name = "Ford",
                Country = "USA",
                Address = "Dearborn, Michigan, USA",
                Cars = new List<Car>
                {
                    new() { Model = "Ford Mustang", Year = 2022, Color = "Red", Price = 55000m },
                    new() { Model = "Ford F-150", Year = 2021, Color = "Blue", Price = 45000m }
                }
            },
            new()
            {
                Name = "Chevrolet",
                Country = "USA",
                Address = "Detroit, Michigan, USA",
                Cars = new List<Car>
                {
                    new() { Model = "Chevrolet Corvette", Year = 2021, Color = "Yellow", Price = 60000m },
                    new() { Model = "Chevrolet Silverado", Year = 2020, Color = "White", Price = 52000m }
                }
            },
            new()
            {
                Name = "Tesla",
                Country = "USA",
                Address = "Palo Alto, California, USA",
                Cars = new List<Car>
                {
                    new() { Model = "Tesla Model S", Year = 2022, Color = "Black", Price = 80000m },
                    new() { Model = "Tesla Model 3", Year = 2021, Color = "White", Price = 35000m }
                }
            }
        };

        // Define known French manufacturers and their cars
        var frenchManufacturers = new List<Manufacturer>
        {
            new()
            {
                Name = "Renault",
                Country = "France",
                Address = "Boulogne-Billancourt, France",
                Cars = new List<Car>
                {
                    new() { Model = "Renault Clio", Year = 2021, Color = "White", Price = 18000m },
                    new() { Model = "Renault Megane", Year = 2020, Color = "Red", Price = 22000m }
                }
            },
            new()
            {
                Name = "Peugeot",
                Country = "France",
                Address = "Paris, France",
                Cars = new List<Car>
                {
                    new() { Model = "Peugeot 208", Year = 2021, Color = "Blue", Price = 20000m },
                    new() { Model = "Peugeot 3008", Year = 2019, Color = "Black", Price = 28000m }
                }
            },
            new()
            {
                Name = "Citroën",
                Country = "France",
                Address = "Saint-Ouen, France",
                Cars = new List<Car>
                {
                    new() { Model = "Citroën C3", Year = 2020, Color = "Green", Price = 17000m },
                    new() { Model = "Citroën C5 Aircross", Year = 2021, Color = "Gray", Price = 32000m }
                }
            }
        };

        // Define known Japanese manufacturers and their cars
        var japaneseManufacturers = new List<Manufacturer>
        {
            new()
            {
                Name = "Toyota",
                Country = "Japan",
                Address = "Toyota City, Aichi, Japan",
                Cars = new List<Car>
                {
                    new() { Model = "Toyota Corolla", Year = 2021, Color = "Silver", Price = 20000m },
                    new() { Model = "Toyota Camry", Year = 2022, Color = "Black", Price = 25000m }
                }
            },
            new()
            {
                Name = "Honda",
                Country = "Japan",
                Address = "Minato, Tokyo, Japan",
                Cars = new List<Car>
                {
                    new() { Model = "Honda Civic", Year = 2021, Color = "Blue", Price = 22000m },
                    new() { Model = "Honda CR-V", Year = 2020, Color = "White", Price = 30000m }
                }
            },
            new()
            {
                Name = "Nissan",
                Country = "Japan",
                Address = "Yokohama, Kanagawa, Japan",
                Cars = new List<Car>
                {
                    new() { Model = "Nissan Altima", Year = 2021, Color = "Red", Price = 24000m },
                    new() { Model = "Nissan Rogue", Year = 2020, Color = "Gray", Price = 26000m }
                }
            }
        };


        // Insert manufacturers and their cars into the database
        var allManufacturers = new List<Manufacturer>();
        allManufacturers.AddRange(germanManufacturers);
        allManufacturers.AddRange(usManufacturers);
        allManufacturers.AddRange(frenchManufacturers);
        allManufacturers.AddRange(japaneseManufacturers);

        foreach (var manufacturer in allManufacturers)
        {
            // Insert each manufacturer
            _manufacturerRepository.Insert(manufacturer);

            // Insert each car associated with the manufacturer
            foreach (var car in manufacturer.Cars)
            {
                car.Manufacturer = manufacturer; // Set the reference to the manufacturer
                _carRepository.Insert(car);
            }
        }

        return Ok("Database seeded with manufacturers and cars.");
    }

    [HttpDelete]
    public IActionResult Clear()
    {
        var manufacturers = _manufacturerRepository.GetAll();
        foreach (var manufacturer in manufacturers)
        {
            var cars = _carRepository.GetAll().Where(c => c.Manufacturer.Id == manufacturer.Id);
            foreach (var car in cars) _carRepository.Delete(car.Id);

            _manufacturerRepository.Delete(manufacturer.Id);
        }

        return Ok("Database cleared.");
    }
}