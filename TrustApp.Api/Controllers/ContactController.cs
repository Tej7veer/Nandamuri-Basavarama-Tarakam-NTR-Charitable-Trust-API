using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;
using TrustApp.Api.Models;

namespace TrustApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly AppDbContext _db;
    public ContactController(AppDbContext db) => _db = db;

    // Admin-facing: list submitted messages
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContactMessage>>> GetAll()
        => Ok(await _db.ContactMessages.OrderByDescending(c => c.SubmittedAt).ToListAsync());

    // Public-facing: submit the "Contact Us" form
    [HttpPost]
    public async Task<ActionResult<ContactMessage>> Create(ContactMessageDto dto)
    {
        var entity = new ContactMessage
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Message = dto.Message
        };
        _db.ContactMessages.Add(entity);
        await _db.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, entity);
    }
}
