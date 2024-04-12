using asp.net.Students.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.net.Students.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class SubjectsController : Controller
    {

        SiteContext _context {  get; set; }
        public SubjectsController(SiteContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                return View(_context.Subjects.Include(s => s.StudentsList).ToList());
            }
            catch (Exception ex)
            {
                return View(_context.Subjects.ToList());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string SubjectName)
        {
            Subject subject = new Subject();
            subject.Name = SubjectName;
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int subjectId, string newSubjectName)
        {
            Subject editSubject = _context.Subjects.FirstOrDefault(s=> s.Id == subjectId);
            if (editSubject == null)
            {
                return NotFound();
            }
            editSubject.Name = newSubjectName;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int subjectId)
        {
            Subject subject = _context.Subjects.FirstOrDefault(s=>s.Id == subjectId);
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
