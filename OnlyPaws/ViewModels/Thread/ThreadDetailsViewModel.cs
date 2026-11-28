namespace OnlyPaws.ViewModels.Thread;

public class ThreadDetailsViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsPinned { get; set; }
    public int ViewsCount { get; set; }
    public int RepliesCount { get; set; }
    
    public Guid ForumId {get; set;} 
    public string ForumName { get; set; } = string.Empty;

    public List<PostViewModel> Posts { get; set; } = new();
    
    public bool CanReply  { get; set; }
    public bool AllowGuestRead { get; set; }
}