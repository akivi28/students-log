using asp.net.Students.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.net.Students.Areas.Student.Controllers;
[Area("Student")]
[Authorize(Roles = "Student, Admin")] 

public class ResultController : Controller
{
    private readonly SiteContext _context;
    private readonly UserManager<User> _userManager;

    public ResultController(SiteContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        if (await _userManager.IsInRoleAsync(user, "Teacher") || await _userManager.IsInRoleAsync(user, "Admin"))
        {
            var results = await _context.QuizStudentResults
                .Include(a => a.Quiz)
                .ThenInclude(q => q.Questions)
                .ThenInclude(x => x.Options)
                .ToListAsync();

            return View(results);
        }
        else
        {
            var userResults = await _context.QuizStudentResults
                .Include(a => a.Quiz)
                .ThenInclude(q => q.Questions)
                .ThenInclude(x => x.Options)
                .Where(a => a.Student.Id == user.Id)
                .ToListAsync();

            return View(userResults);
        }
    }

    public async Task<IActionResult> Show(int id)
    {
        QuizStudentResult result = (await _context.QuizStudentResults
            .Include(r => r.StudentAnswers)
            .Include(r => r.Quiz)
            .ThenInclude(q => q.Questions)
            .ThenInclude(x => x.Options)
            .FirstOrDefaultAsync(r => r.Id == id))!;
        return View(result);
    }
    
}