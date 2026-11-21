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
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<ForumModerator> ForumModerators { get; set; }
    public DbSet<ForumThread> Threads { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ReportedPost> ReportedPosts { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<BannedWord> BannedWords { get; set; }
    
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

        builder.Entity<ForumThread>(e =>
        {
            e.Property(t => t.CreatedAt).HasDefaultValueSql("now()");
            e.HasOne(t => t.Author).WithMany(u => u.Threads)
                .HasForeignKey(t => t.AuthorId).OnDelete(DeleteBehavior.Restrict);
            e.Property(t => t.GuestName).HasMaxLength(50);
            e.ToTable(t => t.HasCheckConstraint(
                "CK_ForumThread_AuthorOrGuest",
                "(\"AuthorId\" IS NULL) <> (\"GuestName\" IS NULL)"));
        });
        
        builder.Entity<ForumModerator>().HasKey(m => new { m.UserId, m.ForumId });
        
        builder.Entity<Post>(e =>
        {
            e.Property(p => p.CreatedAt).HasDefaultValueSql("now()");
            e.HasOne(p => p.Author).WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.Restrict);
            e.Property(p => p.GuestName).HasMaxLength(50);
            e.ToTable(t => t.HasCheckConstraint(
                "CK_Post_AuthorOrGuest",
                "(\"AuthorId\" IS NULL) <> (\"GuestName\" IS NULL)"));
        });

        builder.Entity<Attachment>(e =>
        {
            e.ToTable(t => t.HasCheckConstraint(
                "CK_Attachment_SingleOwner",
                "(\"PostId\" IS NULL) <> (\"MessageId\" IS NULL)"));
        });

        builder.Entity<Message>(e =>
        {
            e.Property(m => m.SentAt).HasDefaultValueSql("now()");
            e.HasOne(m => m.Sender).WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(m => m.Receiver).WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ReportedPost>(e =>
        {
            e.Property(r => r.Status).HasConversion<string>().HasMaxLength(32);
            e.Property(r => r.CreatedAt).HasDefaultValueSql("now()");
            e.HasOne(r => r.Reporter).WithMany()
                .HasForeignKey(r => r.ReporterId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(r => r.Resolver).WithMany()
                .HasForeignKey(r => r.ResolverId).OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<Announcement>(e =>
        {
            e.Property(a => a.CreatedAt).HasDefaultValueSql("now()");
        });

        builder.Entity<BannedWord>(e =>
        {
            e.HasIndex(b => b.Word).IsUnique();
        });
        
    }
}