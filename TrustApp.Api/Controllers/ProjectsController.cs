using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;
using TrustApp.Api.Models;

namespace TrustApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProjectsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectEvent>>> GetAll()
        => Ok(await _db.ProjectEvents.OrderBy(p => p.EventDate).ToListAsync());

    [HttpPost]
    public async Task<ActionResult<ProjectEvent>> Create(ProjectEventDto dto)
    {
        var entity = new ProjectEvent
        {
            Title = dto.Title,
            Description = dto.Description,
            EventDate = dto.EventDate,
            Location = dto.Location,
            IsUpcoming = dto.IsUpcoming
        };
        _db.ProjectEvents.Add(entity);
        await _db.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, entity);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _db.ProjectEvents.FindAsync(id);
        if (entity is null) return NotFound();
        _db.ProjectEvents.Remove(entity);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
