using System.Net;
using System.Net.Mail;
using TrustApp.Api.Models;

namespace TrustApp.Api.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IConfiguration configuration,
        ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendDonationEmailAsync(DonationInquiry donation)
    {
        var smtpHost = _configuration["Email:SmtpHost"];
        var smtpPort = int.Parse(
            _configuration["Email:SmtpPort"] ?? "587");

        var smtpUsername = _configuration["Email:Username"];
        var smtpPassword = _configuration["Email:Password"];

        var fromEmail = _configuration["Email:FromEmail"];
        var toEmail = _configuration["Email:ToEmail"];

        if (string.IsNullOrWhiteSpace(smtpHost) ||
            string.IsNullOrWhiteSpace(smtpUsername) ||
            string.IsNullOrWhiteSpace(smtpPassword) ||
            string.IsNullOrWhiteSpace(fromEmail) ||
            string.IsNullOrWhiteSpace(toEmail))
        {
            throw new InvalidOperationException(
                "Email configuration is incomplete.");
        }

        using var message = new MailMessage();

        message.From = new MailAddress(
            fromEmail,
            "NTR Charitable Trust");

        message.To.Add(
            new MailAddress(toEmail));

        message.Subject =
            $"New Donation Form Submission - ₹{donation.Amount:N2}";

        message.IsBodyHtml = true;

        message.Body = $"""
            <html>
            <body style="font-family:Arial,sans-serif;color:#333">

                <h2 style="color:#8b2635">
                    New Donation Form Submission
                </h2>

                <p>A new donation form has been submitted.</p>

                <table cellpadding="8"
                       cellspacing="0"
                       style="border-collapse:collapse;width:100%;max-width:700px">

                    <tr>
                        <td><strong>Name</strong></td>
                        <td>{Encode(donation.FullName)}</td>
                    </tr>

                    <tr>
                        <td><strong>Mobile No.</strong></td>
                        <td>{Encode(donation.MobileNo)}</td>
                    </tr>

                    <tr>
                        <td><strong>Email</strong></td>
                        <td>{Encode(donation.Email)}</td>
                    </tr>

                    <tr>
                        <td><strong>DOB</strong></td>
                        <td>{FormatDate(donation.Dob)}</td>
                    </tr>

                    <tr>
                        <td><strong>PAN Card No.</strong></td>
                        <td>{Encode(donation.PanCardNo)}</td>
                    </tr>

                    <tr>
                        <td><strong>State</strong></td>
                        <td>{Encode(donation.State)}</td>
                    </tr>

                    <tr>
                        <td><strong>City</strong></td>
                        <td>{Encode(donation.City)}</td>
                    </tr>

                    <tr>
                        <td><strong>PIN Code</strong></td>
                        <td>{Encode(donation.PinCode)}</td>
                    </tr>

                    <tr>
                        <td><strong>Address</strong></td>
                        <td>{Encode(donation.Address)}</td>
                    </tr>

                    <tr>
                        <td><strong>Amount Donated</strong></td>
                        <td>
                            <strong>
                                ₹{donation.Amount:N2}
                            </strong>
                        </td>
                    </tr>

                    <tr>
                        <td><strong>Submitted At</strong></td>
                        <td>
                            {donation.SubmittedAt:dd-MM-yyyy HH:mm:ss} UTC
                        </td>
                    </tr>

                </table>

                <br />

                <p>
                    This email was generated automatically by the
                    Nandamuri Basavarama Tarakam NTR Charitable Trust website.
                </p>

            </body>
            </html>
            """;

        using var smtp = new SmtpClient(
            smtpHost,
            smtpPort);

        smtp.EnableSsl = true;

        smtp.Credentials = new NetworkCredential(
            smtpUsername,
            smtpPassword);

        await smtp.SendMailAsync(message);

        _logger.LogInformation(
            "Donation email sent for donation ID {DonationId}",
            donation.Id);
    }

    private static string Encode(string? value)
    {
        return WebUtility.HtmlEncode(
            string.IsNullOrWhiteSpace(value)
                ? "-"
                : value);
    }

    private static string FormatDate(DateTime? date)
    {
        return date?.ToString("dd-MM-yyyy") ?? "-";
    }
}
