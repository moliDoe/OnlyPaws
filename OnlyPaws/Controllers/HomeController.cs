using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlyPaws.Data;
using OnlyPaws.Models;
using OnlyPaws.ViewModels;

namespace OnlyPaws.Controllers;

public class HomeController : Controller
{
    
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new HomeCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Forums = c.Forums.OrderBy(f => f.Name)
                    .Select(f => new HomeForumViewModel
                    {
                        Id = f.Id,
                        Name = f.Name,
                        Description = f.Description,
                        ThreadCount = f.Threads.Count(),
                        PostCount = f.Threads.SelectMany(th => th.Posts).Count()
                    }).ToList()
            })
            .ToListAsync();
        return View(categories);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}