using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;
using TrustApp.Api.Models;

namespace TrustApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly AppDbContext _db;
    public TeamController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamMember>>> GetAll()
        => Ok(await _db.TeamMembers.OrderBy(t => t.DisplayOrder).ToListAsync());

    [HttpPost]
    public async Task<ActionResult<TeamMember>> Create(TeamMemberDto dto)
    {
        var entity = new TeamMember
        {
            Name = dto.Name,
            Role = dto.Role,
            PhotoUrl = dto.PhotoUrl,
            DisplayOrder = dto.DisplayOrder
        };
        _db.TeamMembers.Add(entity);
        await _db.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, entity);
    }
}
