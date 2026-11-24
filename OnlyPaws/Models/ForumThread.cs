namespace OnlyPaws.Models;

public class ForumThread
{
    public Guid Id { get; set; }
    
    public Guid ForumId { get; set; }
    public Forum Forum { get; set; } = null!;

    public Guid? AuthorId { get; set; }
    public AppUser? Author { get; set; }  
    public string? GuestName { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ViewsCount { get; set; }
    public int RepliesCount { get; set; }
    public bool IsPinned { get; set; }
    
    public List<Post> Posts { get; set; } = new();
}