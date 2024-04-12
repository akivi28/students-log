using asp.net.Students.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.net.Students.Areas.Teacher.Controllers;
[Area("Teacher")]
[Authorize(Roles = "Teacher, Admin")] 
public partial class QuizController : Controller
{
    private readonly SiteContext _context;
    private readonly UserManager<User> _userManager;

    public QuizController(SiteContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Quiz> quizzes = new List<Quiz>();
        quizzes = new List<Quiz>(_context.Quizzes
            .Include(x => x.Subject)
            .Include(x => x.Teacher)
            .Include(x => x.Questions)
            .Include(x=> x.Groups));
        var subjects = await _context.Subjects.ToListAsync();
        ViewData["Subjects"] = subjects;
        var groups = await _context.Groups.ToListAsync();
        ViewData["Groups"] = groups;
        return View(quizzes);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] string QuizName, [FromForm] int QuizSubject, [FromForm] List<int> SelectedGroups)
    {
        var sub = _context.Subjects.FirstOrDefault(s => s.Id == QuizSubject);
        var user = await _userManager.GetUserAsync(User);
        Quiz newQuiz = new Quiz();
        newQuiz.Title = QuizName;
        newQuiz.Subject = sub;
        newQuiz.Teacher = user;
        List<Group> groups = new List<Group>();
        foreach (var groupId in SelectedGroups)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
            if (group != null)
            {
                groups.Add(group);
            }
        }
        newQuiz.Groups = groups;
        await _context.Quizzes.AddAsync(newQuiz);
        await _context.SaveChangesAsync();
        return Redirect("Index");
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var existingQuiz = await _context.Quizzes
            .Include(q => q.Subject)
            .Include(q => q.Groups)
            .Include(q => q.Questions)
            .ThenInclude(x => x.Options)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (existingQuiz == null)
        {
            return NotFound();
        }

        var subjects = await _context.Subjects.ToListAsync();
        var groups = await _context.Groups.ToListAsync();

        ViewData["Subjects"] = subjects;
        ViewData["Groups"] = groups;

        return View(existingQuiz);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string title, int selectedSubjectId, List<int> selectedGroupIds)
    {
        var existingQuiz = await _context.Quizzes
            .Include(q => q.Subject)
            .Include(q => q.Groups)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (existingQuiz == null)
        {
            return NotFound();
        }

        existingQuiz.Title = title;
        existingQuiz.Subject = await _context.Subjects.FindAsync(selectedSubjectId);

        // Clear existing group associations
        existingQuiz.Groups.Clear();

        // Add new group associations
        foreach (var groupId in selectedGroupIds)
        {
            var group = await _context.Groups.FindAsync(groupId);
            if (group != null)
            {
                existingQuiz.Groups.Add(group);
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Edit", new { id = existingQuiz.Id });
    }


    
    public async Task<IActionResult> Delete(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null)
        {
            return NotFound();
        }

        _context.Quizzes.Remove(quiz);
        await _context.SaveChangesAsync();
        return Redirect(Url.Action("Index", "Quiz"));

    }


}