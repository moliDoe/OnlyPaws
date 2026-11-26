namespace OnlyPaws.ViewModels;

public class ForumThreadListViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsPinned { get; set; }
    public int ViewsCount { get; set; }
    public int PostsCount { get; set; }
}