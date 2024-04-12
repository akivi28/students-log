using asp.net.Students.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.net.Students.Controllers;


public class GroupsController : Controller
{
    public SiteContext _context { get; set; }

    public GroupsController(SiteContext context)
    {
        _context = context;
    }
    
    [Authorize(Roles = "Admin,Teacher,Student,Guest")]
    public IActionResult GroupList()
    {
        return View(_context.Groups.Include(g => g.Members).ToList());
    }
    
    [Authorize(Roles = "Admin,Teacher")]
    [HttpGet]
    public IActionResult AddGroup()
    {
        var studentsWithoutGroup = _context.Infos.Where(info => info.GroupId == null).ToList();
        return View(studentsWithoutGroup);
    }

    [Authorize(Roles = "Admin,Teacher")]
    [HttpPost]
    public IActionResult AddGroup(string groupName, int[] selectedStudents)
    {
        Group newGroup = new Group();
        newGroup.Name = groupName;
        newGroup.Members = new List<Info>();
        _context.Groups.Add(newGroup);
        _context.SaveChanges();

        foreach (var Id in selectedStudents)
        {
            var student = _context.Infos.Find(Id);
            if (student != null)
            {
                newGroup.Members.Add(student);
                student.GroupId = newGroup.Id;
            }
        }

        _context.SaveChanges();

        return RedirectToAction("GroupList");
    }

    [Authorize(Roles = "Admin,Teacher,Student")]
    [HttpGet]
    public IActionResult ShowGroup(int id)
    {
        var group = _context.Groups.Include(g => g.Members).ThenInclude(m => m.Photos) 
            .FirstOrDefault(g => g.Id == id);
        return View(group);
    }

    [Authorize(Roles = "Admin,Teacher")]
    [HttpGet]
    public IActionResult EditGroup(int id)
    {
        var group = _context.Groups.Include(g => g.Members).FirstOrDefault(g => g.Id == id);
        return View(group);
    }

    [Authorize(Roles = "Admin,Teacher")]
    [HttpPost]
    public IActionResult EditGroup([FromForm] Group groupModel)
    {
        var group = _context.Groups.FirstOrDefault(g => g.Id == groupModel.Id);

        if (group == null)
        {
            return NotFound();
        }
        group.Name = groupModel.Name;
        _context.SaveChanges();

        return RedirectToAction("ShowGroup", new { id = group.Id });
    }

    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> DeleteFromGroup(int studentId)
    {
        var student = await _context.Infos.Include(i => i.Group)
            .FirstOrDefaultAsync(m => m.Id == studentId);

        if (student == null)
        {
            return NotFound();
        }
        var gId = student.GroupId;
        student.GroupId = null;
        student.Group = null;
        await _context.SaveChangesAsync();
        return RedirectToAction("EditGroup", new { id = gId });
    }

    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> DeleteGroup (int id)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
        if (group == null)
        {
            return NotFound();
        }
        var students = await _context.Infos.Where(s => s.GroupId == id).ToListAsync();
        foreach (var student in students)
        {
            student.GroupId = null;
        }
        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
        return RedirectToAction("GroupList");
    }
}