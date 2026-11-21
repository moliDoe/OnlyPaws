namespace OnlyPaws.Models;

public class Attachment
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
    
    public Guid? PostId { get; set; }
    public Post? Post { get; set; }   
    
    public Guid? MessageId { get; set; }
    public Message? Message { get; set; }
}