using System.Security.Claims;
using asp.net.Students.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.net.Students.Areas.Student.Controllers
{
        [Area("Student")]
        [Authorize(Roles = "Student, Admin")] 
    public class QuizController : Controller
    {
        private readonly SiteContext _context;
        private readonly UserManager<User> _userManager;

        public QuizController(SiteContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
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
            return View(quizzes);
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id)
        {
            Quiz quiz = (await _context.Quizzes
                .Include(x => x.Teacher)
                .Include(x => x.Subject)
                .Include(x => x.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(x => x.Id == id))!;
            return View(quiz);
        }

        [HttpPost]
        public async Task<IActionResult> Show(int quizId, List<int>selectedOptions )
        {
            QuizStudentResult result = new QuizStudentResult();
            User current = await _userManager.FindByEmailAsync(User.Identity.Name);
            result.Student = current;
            result.Quiz = (await _context.Quizzes.FirstOrDefaultAsync(q => q.Id == quizId))!;
            List<QuizStudentAnswer> answers = new List<QuizStudentAnswer>();

            foreach (var selected in selectedOptions)
            {
                QuizStudentAnswer answer = new QuizStudentAnswer();
                answer.Student = current;
                answer.Option = (await _context.QuizOptions.FirstOrDefaultAsync(o => o.Id == selected))!;
                await _context.QuizStudentAnswers.AddAsync(answer);
                result.StudentAnswers.Add(answer);
                answers.Add(answer);
            }
            await _context.SaveChangesAsync();
            result.Score = answers.Sum(a => a.Option.Value);
            result.CreatedAt = DateTime.Now;

            await _context.QuizStudentResults.AddAsync(result);
            await _context.SaveChangesAsync();
            return Json(new { redirectUrl = Url.Action("Show", "Result", new { id = result.Id }) });
        }
    }
}
