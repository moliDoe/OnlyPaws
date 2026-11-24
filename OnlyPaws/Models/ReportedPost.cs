using OnlyPaws.Enums;

namespace OnlyPaws.Models;

public class ReportedPost
{
    public Guid Id { get; set; }
    
    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
    
    public string? Reason { get; set; }
    public ReportedPostStatus Status { get; set; }
    
    public Guid ReporterId { get; set; }
    public AppUser Reporter { get; set; } = null!;  
    
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }
    
    public Guid? ResolverId { get; set; }
    public AppUser? Resolver { get; set; }
}