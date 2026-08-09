using System.ComponentModel.DataAnnotations;

namespace TrustApp.Api.Models;

public record ContactMessageDto(
    [Required, MaxLength(120)] string FullName,
    [Required, EmailAddress, MaxLength(200)] string Email,
    [MaxLength(30)] string? Phone,
    [Required, MaxLength(2000)] string Message
);

public record VolunteerApplicationDto(
    [Required, MaxLength(120)] string FullName,
    [Required, EmailAddress, MaxLength(200)] string Email,
    [Required, MaxLength(30)] string Phone,
    [MaxLength(500)] string? AreaOfInterest,
    [MaxLength(1000)] string? Message
);

public record DonationInquiryDto(
    [Required, MaxLength(120)] string FullName,
    [Required, EmailAddress, MaxLength(200)] string Email,
    [Range(1, 10_000_000)] decimal Amount,
    [MaxLength(500)] string? Note
);

public record ProgramItemDto(
    [Required, MaxLength(150)] string Title,
    [Required, MaxLength(1000)] string Description,
    [MaxLength(60)] string IconKey,
    bool IsLaunched
);

public record ProjectEventDto(
    [Required, MaxLength(150)] string Title,
    [Required, MaxLength(2000)] string Description,
    DateTime? EventDate,
    [MaxLength(200)] string? Location,
    bool IsUpcoming
);

public record GalleryImageDto(
    [Required, MaxLength(200)] string Caption,
    [Required, MaxLength(500)] string ImageUrl,
    bool IsPlaceholder
);

public record TeamMemberDto(
    [Required, MaxLength(120)] string Name,
    [Required, MaxLength(80)] string Role,
    [MaxLength(300)] string? PhotoUrl,
    int DisplayOrder
);

public record CertificateDto(
    [Required, MaxLength(120)] string Name,
    [Required, MaxLength(60)] string Status,
    [MaxLength(300)] string? DocumentUrl,
    int DisplayOrder
);

public record VideoDto(
    [Required, MaxLength(150)] string Caption,
    [Required, MaxLength(300)] string VideoUrl,
    [MaxLength(300)] string? PosterUrl
);

public record NewsPostDto(
    [Required, MaxLength(200)] string Title,
    [Required, MaxLength(4000)] string Body,
    DateTime? PublishedAt
);
