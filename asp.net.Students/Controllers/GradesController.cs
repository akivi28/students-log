using asp.net.Students.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp.net.Students.Controllers
{
    public class GradesController : Controller
    {
        public SiteContext _context;
        public GradesController(SiteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int studentId, int subjectId)
        {
            var data = _context.Grades.Where(g=> g.InfoId == studentId).Where(g => g.SubjectId == subjectId).ToList();
            ViewData["Subject"] = _context.Subjects.FirstOrDefault(s => s.Id == subjectId);
            ViewData["StudentId"] = studentId;
            ViewData["SubjectId"] = subjectId;
            return View(data);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] List<Grade> grades, int studentId, int subjectId)
        {
            var data = _context.Grades.Where(g => g.InfoId == studentId).Where(g => g.SubjectId == subjectId).ToList();
            _context.Grades.RemoveRange(data);

            grades.RemoveAll(grade => grade.Value == 0);
            foreach (var grade in grades)
            {
                grade.InfoId = studentId;
                grade.SubjectId = subjectId;
            }

            _context.AddRange(grades);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { studentId = studentId, subjectId = subjectId });

        }
    }
}
