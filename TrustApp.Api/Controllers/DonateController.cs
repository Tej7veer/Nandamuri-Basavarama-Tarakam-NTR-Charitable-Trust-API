using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;
using TrustApp.Api.Models;

namespace TrustApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DonateController : ControllerBase
{
    private readonly AppDbContext _db;
    public DonateController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DonationInquiry>>> GetAll()
        => Ok(await _db.DonationInquiries.OrderByDescending(d => d.SubmittedAt).ToListAsync());

    // NOTE: this only records donor interest. Wire this up to a real
    // payment gateway (Razorpay/Stripe/PayPal) before going live.
    [HttpPost]
    public async Task<ActionResult<DonationInquiry>> Create(DonationInquiryDto dto)
    {
        var entity = new DonationInquiry
        {
            FullName = dto.FullName,
            Email = dto.Email,
            Amount = dto.Amount,
            Note = dto.Note
        };
        _db.DonationInquiries.Add(entity);
        await _db.SaveChangesAsync();
        return StatusCode(StatusCodes.Status201Created, entity);
    }
}
