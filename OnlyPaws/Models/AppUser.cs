using Microsoft.AspNetCore.Identity;
using OnlyPaws.Enums;

namespace OnlyPaws.Models;

public class AppUser : IdentityUser<Guid>
{
    public PetTag Tag { get; set; }
    public string? AvatarPath { get; set; }
    public UserRank Rank { get; set; }
    public int PawsCount { get; set; }
    public int ActivityCount { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public List<ForumThread> Threads { get; set; } = new();
    public List<Post> Posts { get; set; } = new();
    public List<Message> SentMessages { get; set; } = new();
    public List<Message> ReceivedMessages { get; set; } = new();
    public List<ForumModerator> ModeratedForums { get; set; } = new();
}