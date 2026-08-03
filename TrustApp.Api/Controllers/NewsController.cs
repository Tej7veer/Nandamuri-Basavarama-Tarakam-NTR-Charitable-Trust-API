using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;
using TrustApp.Api.Models;

namespace TrustApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly AppDbContext _db;
    public NewsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NewsPost>>> GetAll()
        => Ok(await _db.NewsPosts.OrderByDescending(n => n.PublishedAt).ToListAsync());

    [HttpPost]
    public async Task<ActionResult<NewsPost>> Create(NewsPostDto dto)
    {
        var entity = new NewsPost
        {
            Title = dto.Title,
            Body = dto.Body,
            PublishedAt = dto.PublishedAt ?? DateTime.UtcNow
        };
        _db.NewsPosts.Add(entity);
        await _db.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, entity);
    }
}
