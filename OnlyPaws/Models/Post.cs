namespace OnlyPaws.Models;

public class Post
{
    public Guid Id { get; set; }
    
    public Guid? AuthorId { get; set; }
    public AppUser? Author { get; set; } = null!;
    public string? GuestName { get; set; }
    
    public Guid ThreadId { get; set; }
    public ForumThread Thread { get; set; } = null!;
    
    public string Content { get; set; }
    public DateTime CreatedAt { get; set;}
    public int PawsCount { get; set; }
    
    public List<Attachment> Attachments { get; set; } = new(); 
    public List<ReportedPost> Reports { get; set; } = new();
    
    
    
}