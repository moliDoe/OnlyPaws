using System.ComponentModel.DataAnnotations;

namespace OnlyPaws.ViewModels.Thread;

public class PostCreateViewModel
{
    public Guid ThreadId { get; set; }
    [Required, StringLength(5000)]
    public string Content { get; set; } = string.Empty;
    public string? GuestName { get; set; }
}