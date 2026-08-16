using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;
using TrustApp.Api.Models;
using TrustApp.Api.Services;

namespace TrustApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DonateController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IEmailService _emailService;
    private readonly ILogger<DonateController> _logger;

    public DonateController(
        AppDbContext db,
        IEmailService emailService,
        ILogger<DonateController> logger)
    {
        _db = db;
        _emailService = emailService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DonationInquiry>>> GetAll()
    {
        return Ok(
            await _db.DonationInquiries
                .OrderByDescending(d => d.SubmittedAt)
                .ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult> Create(
        DonationInquiryDto dto)
    {
        var entity = new DonationInquiry
        {
            FullName = dto.FullName.Trim(),
            MobileNo = dto.MobileNo.Trim(),
            Email = dto.Email.Trim(),
            Dob = dto.Dob,
            PanCardNo = string.IsNullOrWhiteSpace(dto.PanCardNo)
                ? null
                : dto.PanCardNo.Trim().ToUpperInvariant(),
            State = dto.State.Trim(),
            City = dto.City.Trim(),
            PinCode = dto.PinCode.Trim(),
            Address = dto.Address.Trim(),
            Amount = dto.Amount,
            SubmittedAt = DateTime.UtcNow
        };

        _db.DonationInquiries.Add(entity);

        await _db.SaveChangesAsync();

        try
        {
            await _emailService.SendDonationEmailAsync(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Donation saved but email could not be sent. Donation ID: {DonationId}",
                entity.Id);

            // Important:
            // Donation is already saved.
            // We don't return 500 because the donor submission succeeded.
        }

        return StatusCode(
            StatusCodes.Status201Created,
            new
            {
                message = "Donation details submitted successfully.",
                id = entity.Id
            });
    }
}
