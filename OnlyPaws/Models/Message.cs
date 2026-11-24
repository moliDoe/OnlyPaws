namespace OnlyPaws.Models;

public class Message
{
    public Guid Id { get; set; }
    
    public Guid SenderId { get; set; }
    public AppUser Sender { get; set; } = null!;  
    
    public Guid ReceiverId { get; set; }
    public AppUser Receiver { get; set; } = null!;
    
    public string Content { get; set; }
    public DateTime SentAt { get; set; }
    public DateTime? ReadAt { get; set; }
    public bool IsDeletedBySender { get; set; }
    public bool IsDeletedByReceiver { get; set; }
    
    public List<Attachment> Attachments { get; set; } = new(); 
}