using Microsoft.AspNetCore.Mvc;
using NoSqlCRUDAPI.Database;
using NoSqlCRUDAPI.Models;

namespace NoSqlCRUDAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManufacturerController : ControllerBase
{
    private readonly Repository<Manufacturer> _repository;
    
    public ManufacturerController(Repository<Manufacturer> repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public IActionResult GetAllAuthors()
    {
        var authors = _repository.GetAll();
        return Ok(authors);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetAuthorById(Guid id)
    {
        var author = _repository.GetById(id);
        if (author == null)
            return NotFound();
        return Ok(author);
    }
    
    [HttpPost]
    public IActionResult CreateAuthor(Manufacturer manufacturer)
    {
        _repository.Insert(manufacturer);
        return CreatedAtAction(nameof(GetAuthorById), new { id = manufacturer.Id }, manufacturer);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(Guid id, Manufacturer manufacturer)
    {
        if (id != manufacturer.Id)
            return BadRequest();

        var updated = _repository.Update(manufacturer);
        if (!updated)
            return NotFound();

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(Guid id)
    {
        var deleted = _repository.Delete(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}