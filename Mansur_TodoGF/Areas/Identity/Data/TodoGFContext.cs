using Mansur_TodoGF.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mansur_TodoGF.Areas.Identity.Data;

public class TodoGFContext : IdentityDbContext<ApplicationUser>
{
    public TodoGFContext(DbContextOptions<TodoGFContext> options)
        : base(options)
    {
    }
    public DbSet<Todo> Todo { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Todo>().ToTable("Todo");
    }
}
