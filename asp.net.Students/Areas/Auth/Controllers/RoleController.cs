using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace asp.net.Students.Areas.Auth.Controllers;
[Area("Auth")]
[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public RoleController(RoleManager<IdentityRole<int>> roleManager)
    {
        _roleManager = roleManager;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View(_roleManager.Roles.ToList());
    }
}