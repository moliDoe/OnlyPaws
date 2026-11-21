namespace OnlyPaws.Models;

public class ForumModerator
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;
    public Guid ForumId { get; set; }
    public Forum Forum { get; set; } = null!;
}