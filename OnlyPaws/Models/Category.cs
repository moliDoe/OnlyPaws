using System.ComponentModel.DataAnnotations;

namespace OnlyPaws.Models;

public class Category
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Category name is required!")]
    public string Name { get; set; }
    
    public List<Forum> Forums { get; set; } = new();
}