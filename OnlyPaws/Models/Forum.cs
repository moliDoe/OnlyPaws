namespace OnlyPaws.Models;

public class Forum
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool AllowGuestRead { get; set; }
    public bool AllowGuestWrite { get; set; }
    public int ThreadCount { get; set; }
    public int PostCount { get; set; }
    
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    
    public List<ForumThread> Threads { get; set; } = new();
    public List<ForumModerator> Moderators { get; set; } = new();
    public List<Announcement> Announcements { get; set; } = new();
}