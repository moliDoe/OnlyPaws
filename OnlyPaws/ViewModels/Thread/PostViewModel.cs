namespace OnlyPaws.ViewModels.Thread;

public class PostViewModel
{
    public Guid Id { get; set; }
    public bool IsAuthorGuest { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}