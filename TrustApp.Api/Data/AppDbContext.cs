using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Models;

namespace TrustApp.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ProgramItem> Programs => Set<ProgramItem>();
    public DbSet<ProjectEvent> ProjectEvents => Set<ProjectEvent>();
    public DbSet<GalleryImage> GalleryImages => Set<GalleryImage>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<VolunteerApplication> VolunteerApplications => Set<VolunteerApplication>();
    public DbSet<DonationInquiry> DonationInquiries => Set<DonationInquiry>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<Certificate> Certificates => Set<Certificate>();
    public DbSet<Video> Videos => Set<Video>();
    public DbSet<NewsPost> NewsPosts => Set<NewsPost>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ensure decimal precision for Amount to avoid silent truncation.
        modelBuilder.Entity<DonationInquiry>()
            .Property(d => d.Amount)
            .HasPrecision(18, 2);

        // Seed a few "coming soon" programs so the Programs page isn't empty on first run
        modelBuilder.Entity<ProgramItem>().HasData(
            new ProgramItem { Id = 1, Title = "Education", Description = "Supporting learning resources and school access for underserved children.", IconKey = "graduation-cap", IsLaunched = false, CreatedAt = new DateTime(2026, 1, 1) },
            new ProgramItem { Id = 2, Title = "Healthcare", Description = "Planned health camps and awareness drives in local communities.", IconKey = "heart-pulse", IsLaunched = false, CreatedAt = new DateTime(2026, 1, 1) },
            new ProgramItem { Id = 3, Title = "Food Support", Description = "Meal and ration support programs for families in need.", IconKey = "utensils", IsLaunched = false, CreatedAt = new DateTime(2026, 1, 1) },
            new ProgramItem { Id = 4, Title = "Environment", Description = "Tree plantation and cleanliness drives, launching soon.", IconKey = "leaf", IsLaunched = false, CreatedAt = new DateTime(2026, 1, 1) },
            new ProgramItem { Id = 5, Title = "Women Empowerment", Description = "Skill-building and livelihood support initiatives for women.", IconKey = "users", IsLaunched = false, CreatedAt = new DateTime(2026, 1, 1) }
        );

        // Seed placeholder trustees so "Our Team" isn't empty on first run - replace
        // Name/Role/PhotoUrl with the real trustees via PUT once known.
        modelBuilder.Entity<TeamMember>().HasData(
            new TeamMember { Id = 1, Name = "Trustee Name", Role = "Chairman", PhotoUrl = null, DisplayOrder = 1, CreatedAt = new DateTime(2026, 1, 1) },
            new TeamMember { Id = 2, Name = "Trustee Name", Role = "President", PhotoUrl = null, DisplayOrder = 2, CreatedAt = new DateTime(2026, 1, 1) },
            new TeamMember { Id = 3, Name = "Trustee Name", Role = "Secretary", PhotoUrl = null, DisplayOrder = 3, CreatedAt = new DateTime(2026, 1, 1) },
            new TeamMember { Id = 4, Name = "Trustee Name", Role = "Treasurer", PhotoUrl = null, DisplayOrder = 4, CreatedAt = new DateTime(2026, 1, 1) }
        );

        // Seed certificate status placeholders - update Status/DocumentUrl via PUT once
        // each document is actually issued. Never auto-generate DocumentUrl content.
        modelBuilder.Entity<Certificate>().HasData(
            new Certificate { Id = 1, Name = "Trust Registration", Status = "Pending upload", DocumentUrl = null, DisplayOrder = 1, CreatedAt = new DateTime(2026, 1, 1) },
            new Certificate { Id = 2, Name = "80G Certificate", Status = "Applied for", DocumentUrl = null, DisplayOrder = 2, CreatedAt = new DateTime(2026, 1, 1) },
            new Certificate { Id = 3, Name = "12A Certificate", Status = "Applied for", DocumentUrl = null, DisplayOrder = 3, CreatedAt = new DateTime(2026, 1, 1) },
            new Certificate { Id = 4, Name = "PAN Card", Status = "Pending upload", DocumentUrl = null, DisplayOrder = 4, CreatedAt = new DateTime(2026, 1, 1) }
        );

        // Videos and News are intentionally left unseeded, like Gallery and Projects -
        // they represent activity a newly-formed trust doesn't have yet. The frontend
        // shows an honest empty state until real entries are added.
    }
}
