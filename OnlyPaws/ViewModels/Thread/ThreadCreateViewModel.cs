using System.ComponentModel.DataAnnotations;

namespace OnlyPaws.ViewModels.Thread;

public class ThreadCreateViewModel
{
    public Guid ForumId { get; set; } 
    [Required, StringLength(150, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    
    [Required, StringLength(2500)]
    public string Content { get; set; } = string.Empty;
    
    public string? GuestName { get; set; } 
    public bool IsPinned { get; set; } 
    
    public string ForumName { get; set; } = string.Empty;
}