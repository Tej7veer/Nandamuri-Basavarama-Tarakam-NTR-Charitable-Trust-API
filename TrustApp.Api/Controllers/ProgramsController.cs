using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;
using TrustApp.Api.Models;

namespace TrustApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgramsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProgramsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProgramItem>>> GetAll()
        => Ok(await _db.Programs.OrderBy(p => p.Id).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProgramItem>> GetById(int id)
    {
        var item = await _db.Programs.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ProgramItem>> Create(ProgramItemDto dto)
    {
        var entity = new ProgramItem
        {
            Title = dto.Title,
            Description = dto.Description,
            IconKey = dto.IconKey,
            IsLaunched = dto.IsLaunched
        };
        _db.Programs.Add(entity);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProgramItemDto dto)
    {
        var entity = await _db.Programs.FindAsync(id);
        if (entity is null) return NotFound();

        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.IconKey = dto.IconKey;
        entity.IsLaunched = dto.IsLaunched;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.Programs.FindAsync(id);
        if (entity is null) return NotFound();

        _db.Programs.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
