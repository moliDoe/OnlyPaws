namespace OnlyPaws.ViewModels;

public class ForumDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public bool AllowGuestRead { get; set; }
    public bool AllowGuestWrite { get; set; }
    
    public List<ForumThreadListViewModel> Threads { get; set; }
}