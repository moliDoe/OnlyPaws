using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlyPaws.Data;
using OnlyPaws.Models;
using OnlyPaws.ViewModels;

namespace OnlyPaws.Controllers;

public class ForumController : Controller
{
    private readonly ApplicationDbContext _context;

    public ForumController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var forums = await _context.Forums
            .OrderBy(f => f.Name)
            .ToListAsync();
        
        return View(forums);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new ForumCreateViewModel
        {
            Categories = await GetCategories()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ForumCreateViewModel model)
    {

        if (model.CategoryId != null && !await _context.Categories.AnyAsync(c => c.Id == model.CategoryId))
        {
            ModelState.AddModelError(nameof(model.CategoryId), "Category not found");
        }
        
        
        if (!ModelState.IsValid)
        {
            model.Categories = await GetCategories();
            return View(model);
        }

        var newForum = new Forum{
            Name = model.Name.Trim(),
            Description = model.Description?.Trim(),
            CategoryId = model.CategoryId,
            AllowGuestWrite = model.AllowGuestWrite,
            AllowGuestRead = model.AllowGuestRead
        };
        
        _context.Forums.Add(newForum);
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));
        
    }

    private async Task<List<SelectListItem>> GetCategories()
    {
        return await _context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
            .ToListAsync();
    }

    [HttpGet]
    public async Task<ActionResult> Details(Guid id)
    {
        var forum = await _context.Forums
            .Where(forum  => forum.Id == id)
            .Select(fd => new ForumDetailsViewModel
            {
                Id = fd.Id,
                Name = fd.Name,
                Description = fd.Description,
                CategoryName =  fd.Category.Name,
                AllowGuestWrite = fd.AllowGuestWrite,
                AllowGuestRead = fd.AllowGuestRead,
                Threads = fd.Threads.Select(
                    th => new ForumThreadListViewModel
                    {
                        Id = th.Id,
                        Title = th.Title,
                        AuthorName = th.Author != null ? th.Author.UserName! : (th.GuestName ?? "Guest"),
                        CreatedAt = th.CreatedAt,
                        IsPinned = th.IsPinned,
                        ViewsCount = th.ViewsCount,
                        PostsCount = th.Posts.Count()
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        if (forum == null)
        {
            return NotFound();
        }
        
        var isAuthenticated = User.Identity!.IsAuthenticated;

        if (!forum.AllowGuestRead && !isAuthenticated)
        {
            return Unauthorized();
        }

        return View(forum);
    }
    
    
    
}