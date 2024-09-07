using LiteDB;

namespace NoSqlCRUDAPI.Models;

public class Manufacturer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Country { get; set; }
    public string Address { get; set; }

    [BsonRef("cars")] public List<Car> Cars { get; set; } = [];
}