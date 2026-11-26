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
        if (!ModelState.IsValid)
        {
            model.Categories = await GetCategories();
            return View(model);
        }

        var newForum = new Forum{
            Name = model.Name.Trim(),
            Description = model.Description.Trim(),
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
    
}