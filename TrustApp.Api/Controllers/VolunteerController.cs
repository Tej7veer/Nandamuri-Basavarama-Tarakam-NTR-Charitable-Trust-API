using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;
using TrustApp.Api.Models;

namespace TrustApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VolunteerController : ControllerBase
{
    private readonly AppDbContext _db;
    public VolunteerController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VolunteerApplication>>> GetAll()
        => Ok(await _db.VolunteerApplications.OrderByDescending(v => v.SubmittedAt).ToListAsync());

    [HttpPost]
    public async Task<ActionResult<VolunteerApplication>> Create(VolunteerApplicationDto dto)
    {
        var entity = new VolunteerApplication
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            AreaOfInterest = dto.AreaOfInterest,
            Message = dto.Message
        };
        _db.VolunteerApplications.Add(entity);
        await _db.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, entity);
    }
}
