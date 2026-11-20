using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlyPaws.Models;

namespace OnlyPaws.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>(e =>
        {
            e.Property(u => u.Tag)
                .HasConversion<string>()
                .HasMaxLength(32);

            e.Property(u => u.Rank)
                .HasConversion<string>()
                .HasMaxLength(32);

            e.Property(u => u.CreatedAt)
                .HasDefaultValueSql("now()");
        });
    }
}