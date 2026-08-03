namespace TrustApp.Api.Models;

public record ContactMessageDto(string FullName, string Email, string? Phone, string Message);

public record VolunteerApplicationDto(string FullName, string Email, string Phone, string? AreaOfInterest, string? Message);

public record DonationInquiryDto(string FullName, string Email, decimal Amount, string? Note);

public record ProgramItemDto(string Title, string Description, string IconKey, bool IsLaunched);

public record ProjectEventDto(string Title, string Description, DateTime? EventDate, string? Location, bool IsUpcoming);

public record GalleryImageDto(string Caption, string ImageUrl, bool IsPlaceholder);

public record TeamMemberDto(string Name, string Role, string? PhotoUrl, int DisplayOrder);

public record CertificateDto(string Name, string Status, string? DocumentUrl, int DisplayOrder);

public record VideoDto(string Caption, string VideoUrl, string? PosterUrl);

public record NewsPostDto(string Title, string Body, DateTime? PublishedAt);
