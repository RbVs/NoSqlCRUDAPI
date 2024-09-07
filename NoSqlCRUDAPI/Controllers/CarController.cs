using Microsoft.AspNetCore.Mvc;
using NoSqlCRUDAPI.Database;
using NoSqlCRUDAPI.Models;

namespace NoSqlCRUDAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly Repository<Car> _repository;

    public CarController(Repository<Car> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _repository.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(Guid id)
    {
        var product = _repository.GetById(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct(Car car)
    {
        _repository.Insert(car);
        return CreatedAtAction(nameof(GetProductById), new { id = car.Id }, car);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(Guid id, Car car)
    {
        if (id != car.Id)
            return BadRequest();

        var updated = _repository.Update(car);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(Guid id)
    {
        var deleted = _repository.Delete(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}