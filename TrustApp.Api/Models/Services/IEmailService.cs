using TrustApp.Api.Models;

namespace TrustApp.Api.Services;

public interface IEmailService
{
    Task SendDonationEmailAsync(DonationInquiry donation);
}
