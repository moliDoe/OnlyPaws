namespace OnlyPaws.ViewModels;

public class HomeForumViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } 
    public int ThreadCount { get; set; }
    public int PostCount { get; set; }
}