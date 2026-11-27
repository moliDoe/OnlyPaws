using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlyPaws.Data;
using OnlyPaws.Models;
using OnlyPaws.ViewModels.Thread;

namespace OnlyPaws.Controllers;

public class ForumThreadController : Controller
{
    
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    
    public ForumThreadController(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
   

    [HttpGet("Forum/{forumId:guid}/Thread/Create")]
    public async Task<IActionResult> Create(Guid forumId)
    {
        var forum = await _context.Forums.FindAsync(forumId);
        if (forum == null)
        {
            return NotFound();
        }
        var isAutenticated = User.Identity!.IsAuthenticated;
        if (!isAutenticated && !forum.AllowGuestWrite)
        {
            return Unauthorized();
        }

        return View(new ThreadCreateViewModel
        {
            ForumId = forum.Id, 
            ForumName = forum.Name
        });
    }
    
    
    [HttpPost("Forum/{forumId:guid}/Thread/Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ThreadCreateViewModel model)
    {
        var forum = await _context.Forums.FindAsync(model.ForumId);
        if (forum == null)
        {
            return NotFound();
        }
        
        var isAutenticated = User.Identity!.IsAuthenticated;
        if (!isAutenticated && !forum.AllowGuestWrite)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            model.ForumName = forum.Name;
            return View(model);
        }

        Guid? authorId;
        string? guestName;

        if (isAutenticated)
        {
            authorId = Guid.Parse(_userManager.GetUserId(User)!);
            guestName = null;
        }
        else
        {
            authorId = null;
            guestName = model.GuestName ?? "Guest";
        }
        
        var createdDate = DateTime.UtcNow;

        var newThread = new ForumThread
        {
            ForumId = model.ForumId,
            Title = model.Title.Trim(),
            AuthorId = authorId,
            GuestName = guestName,
            CreatedAt = createdDate,
            IsPinned = model.IsPinned && User.IsInRole("Admin"),
            Posts =
            {
                new Post
                {
                    Content = model.Content,
                    AuthorId = authorId,
                    GuestName = guestName,
                    CreatedAt = createdDate
                }
            }
        };
        
        _context.Threads.Add(newThread);
        forum.ThreadCount++;
        forum.PostCount++;

        await _context.SaveChangesAsync();
        
        return RedirectToAction("Details", "Forum", new { id = forum.Id });
    }

    [HttpGet("Thread/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var isAuthenticated = User.Identity!.IsAuthenticated;

        var myThread = await _context.Threads
            .Where(th => th.Id == id)
            .Select(td => new ThreadDetailsViewModel
            {
                Id = td.Id,
                Title = td.Title,
                IsPinned = td.IsPinned,
                ViewsCount = td.ViewsCount,
                RepliesCount = td.RepliesCount,
                ForumId = td.ForumId,
                ForumName = td.Forum.Name,
                CanReply = isAuthenticated || td.Forum.AllowGuestWrite,
                Posts = td.Posts.Where(p => p.ThreadId == td.Id)
                    .Select(p => new PostViewModel
                    {
                        Id = p.Id,
                        IsAuthorGuest = p.Author == null,
                        AuthorName = p.Author != null ? p.Author.UserName : (p.GuestName ?? "Guest"),
                        Content = p.Content,
                        CreatedAt = p.CreatedAt
                    })
                    .ToList<PostViewModel>()

            })
            .FirstOrDefaultAsync();

        
        if (myThread == null)
        {
            return NotFound();
        }

        if (!isAuthenticated)
        {
            return Challenge();
        }

        
        myThread.ViewsCount++;
        await _context.SaveChangesAsync();
        
        return View(myThread);
    }
    
    [HttpPost("Thread/{id:guid}/Reply")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reply(Guid id, PostCreateViewModel model)
    {
        var myThread = await _context.Threads
            .Where(th => th.Id == id)
            .Include(t => t.Forum)
            .FirstOrDefaultAsync();
        
        if (myThread == null)
        {
            return NotFound();
        }

        var isAuthenticated = User.Identity!.IsAuthenticated;
        if (!isAuthenticated && !myThread.Forum.AllowGuestWrite)
        {
            return Challenge();
        }

        Guid? postAuthorId;
        string? guestName;

        if (isAuthenticated)
        {
            postAuthorId = Guid.Parse(_userManager.GetUserId(User)!);
            guestName = null;
        }
        else
        {
            postAuthorId = null;
            guestName = model.GuestName ?? "Guest";
        }

        var post = new Post
        {
            ThreadId = myThread.Id,
            Content = model.Content,
            CreatedAt = DateTime.UtcNow,
            AuthorId = postAuthorId,
            GuestName = guestName
        };

        _context.Posts.Add(post);
        myThread.RepliesCount++;
        myThread.Forum.PostCount++;

        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id });

    }
    
    
  
}