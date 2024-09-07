using LiteDB;

namespace NoSqlCRUDAPI.Models;

public class Car
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Model { get; set; }  
    public int Year { get; set; }      
    public string Color { get; set; }  
    public decimal Price { get; set; } 
    
    [BsonRef("manufacturer")]
    public Manufacturer Manufacturer { get; set; } = new Manufacturer();
}