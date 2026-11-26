namespace OnlyPaws.ViewModels;

public class HomeCategoryViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<HomeForumViewModel> Forums { get; set; }
}