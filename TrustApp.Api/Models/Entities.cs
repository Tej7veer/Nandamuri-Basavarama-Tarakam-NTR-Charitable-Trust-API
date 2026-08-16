using System.ComponentModel.DataAnnotations;

namespace TrustApp.Api.Models;

// A planned/ongoing initiative shown on the "Programs" page
// e.g. Education, Healthcare, Food Support, Environment, Women Empowerment
public class ProgramItem
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    // e.g. "graduation-cap", "heart-pulse" - used to pick an icon on the frontend
    [MaxLength(60)]
    public string IconKey { get; set; } = "sparkles";

    public bool IsLaunched { get; set; } = false; // false => shows "Coming Soon" badge

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// An upcoming project or event, shown on "Projects & Events"
public class ProjectEvent
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    public DateTime? EventDate { get; set; } // null => "date to be announced"

    [MaxLength(200)]
    public string? Location { get; set; }

    public bool IsUpcoming { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// Gallery placeholder / real photo metadata
public class GalleryImage
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Caption { get; set; } = string.Empty;

    [Required, MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    public bool IsPlaceholder { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// Contact Us form submissions
public class ContactMessage
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(30)]
    public string? Phone { get; set; }

    [Required, MaxLength(2000)]
    public string Message { get; set; } = string.Empty;

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}

// "Become a Volunteer" applications
public class VolunteerApplication
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(30)]
    public string Phone { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? AreaOfInterest { get; set; } // e.g. "Education", "Events"

    [MaxLength(1000)]
    public string? Message { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}

// A donation pledge / interest record (no real payment processing in this sample)
public class DonationInquiry
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required, MaxLength(30)]
    public string MobileNo { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    public DateTime? Dob { get; set; }

    [MaxLength(10)]
    public string? PanCardNo { get; set; }

    [Required, MaxLength(100)]
    public string State { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string City { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public string PinCode { get; set; } = string.Empty;

    [Required, MaxLength(1000)]
    public string Address { get; set; } = string.Empty;

    [Range(1, 10_000_000)]
    public decimal Amount { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}

// A trustee/team member shown in "Our Team" on the About page
public class TeamMember
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(80)]
    public string Role { get; set; } = string.Empty; // e.g. "Chairman", "President"

    // Relative path under assets/, e.g. "assets/team/chairman.jpg". Null until a real
    // photo is uploaded - the frontend falls back to a placeholder circle.
    [MaxLength(300)]
    public string? PhotoUrl { get; set; }

    public int DisplayOrder { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// A registration/tax-exemption document status shown on the About page.
// Deliberately status-only (no generated certificate images) - see DocumentUrl comment.
public class Certificate
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Name { get; set; } = string.Empty; // e.g. "80G Certificate"

    [Required, MaxLength(60)]
    public string Status { get; set; } = "Pending upload"; // e.g. "Applied for", "Verified"

    // Only ever set once a REAL scanned certificate is uploaded by the trust - this API
    // never generates or fabricates certificate documents/images itself.
    [MaxLength(300)]
    public string? DocumentUrl { get; set; }

    public int DisplayOrder { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// A video shown in the Gallery page's Videos section
public class Video
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Caption { get; set; } = string.Empty;

    [Required, MaxLength(300)]
    public string VideoUrl { get; set; } = string.Empty; // e.g. "assets/videos/video-1.mp4"

    [MaxLength(300)]
    public string? PosterUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// A News & Updates post
public class NewsPost
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(4000)]
    public string Body { get; set; } = string.Empty;

    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
}
