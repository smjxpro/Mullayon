using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mullayon.Core.Constants;
using Mullayon.Core.Entities;

namespace Mullayon.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var adminRole = new ApplicationRole
        {
            Id = Guid.NewGuid().ToString(),
            Name = RoleStrings.Admin,
            NormalizedName = "ADMIN"
        };

        var userRole = new ApplicationRole
        {
            Id = Guid.NewGuid().ToString(),
            Name = RoleStrings.User,
            NormalizedName = "USER"
        };

        builder.Entity<ApplicationRole>().HasData(new List<ApplicationRole>
        {
            adminRole,
            userRole
        });
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Image> Images { get; set; }
}