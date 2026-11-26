using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OnlyPaws.ViewModels;

public class ForumCreateViewModel
{

    [Required(ErrorMessage = "Forum name is required!")]
    [StringLength(50, MinimumLength = 3,  ErrorMessage = "Forum name must be between 3 and 50 characters")]
    [Display(Name = "Forum name")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    [Display(Name = "Description")]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Category is required!")]
    [Display(Name = "Category")]
    public Guid CategoryId { get; set; }
    
    [Display(Name = "Guests can write messages")]
    public bool AllowGuestWrite { get; set; } = false;
    
    [Display(Name = "Guests can view messages")]
    public bool AllowGuestRead { get; set; } = true;


    public List<SelectListItem> Categories { get; set; } = [];


}