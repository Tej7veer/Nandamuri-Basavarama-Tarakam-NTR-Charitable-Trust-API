using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;
using TrustApp.Api.Models;

namespace TrustApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CertificatesController : ControllerBase
{
    private readonly AppDbContext _db;
    public CertificatesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Certificate>>> GetAll()
        => Ok(await _db.Certificates.OrderBy(c => c.DisplayOrder).ToListAsync());

    [HttpPost]
    public async Task<ActionResult<Certificate>> Create(CertificateDto dto)
    {
        var entity = new Certificate
        {
            Name = dto.Name,
            Status = dto.Status,
            DocumentUrl = dto.DocumentUrl,
            DisplayOrder = dto.DisplayOrder
        };
        _db.Certificates.Add(entity);
        await _db.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, entity);
    }
}
