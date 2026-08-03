using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;
using TrustApp.Api.Models;

namespace TrustApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GalleryController : ControllerBase
{
    private readonly AppDbContext _db;
    public GalleryController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GalleryImage>>> GetAll()
        => Ok(await _db.GalleryImages.OrderByDescending(g => g.CreatedAt).ToListAsync());

    [HttpPost]
    public async Task<ActionResult<GalleryImage>> Create(GalleryImageDto dto)
    {
        var entity = new GalleryImage
        {
            Caption = dto.Caption,
            ImageUrl = dto.ImageUrl,
            IsPlaceholder = dto.IsPlaceholder
        };
        _db.GalleryImages.Add(entity);
        await _db.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, entity);
    }
}
