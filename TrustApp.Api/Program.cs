using Microsoft.EntityFrameworkCore;
using TrustApp.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// EF Core - SQL Server. Update the "Default" connection string in appsettings.json
// if your server/instance name differs.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow the Angular dev server (ng serve, default port 4200) to call the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// If you ran database-script.sql yourself, tables already exist and this just
// verifies that and does nothing further. If not, EnsureCreated() creates the
// schema + seed data itself, matching that script exactly - either path works.
// NOTE: EnsureCreated() doesn't support incremental schema changes. For real
// production use, switch to migrations: `dotnet ef migrations add InitialCreate`
// then replace this with `db.Database.Migrate()`.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularDev");
app.UseHttpsRedirection();

// Serve a production Angular build if one has been copied into wwwroot/ (see README
// "Combined deployment"). Harmless no-op in the normal dev workflow, where wwwroot
// doesn't exist and ng serve handles the frontend separately on port 4200.
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

// Updated fallback: exclude api/* and swagger-related paths so Swagger UI and its JSON are not overridden.
app.MapFallbackToFile("{*path:regex(^(?!(api/|swagger|swagger-ui)).*$)}", "index.html");

app.Run();
