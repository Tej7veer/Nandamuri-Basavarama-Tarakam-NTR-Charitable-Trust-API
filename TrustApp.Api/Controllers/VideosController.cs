using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;
using TrustApp.Api.Models;

namespace TrustApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideosController : ControllerBase
{
    private readonly AppDbContext _db;
    public VideosController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Video>>> GetAll()
        => Ok(await _db.Videos.OrderByDescending(v => v.CreatedAt).ToListAsync());

    [HttpPost]
    public async Task<ActionResult<Video>> Create(VideoDto dto)
    {
        var entity = new Video
        {
            Caption = dto.Caption,
            VideoUrl = dto.VideoUrl,
            PosterUrl = dto.PosterUrl
        };
        _db.Videos.Add(entity);
        await _db.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, entity);
    }
}
