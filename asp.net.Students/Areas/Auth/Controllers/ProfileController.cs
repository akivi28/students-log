using System.Security.Claims;
using asp.net.Students.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace asp.net.Students.Areas.Auth.Controllers;
[Area("Auth")]
[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<Students.Models.User> _userManager;
    
    public ProfileController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserRoles()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound("Unable to load user.");
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        return Json(userRoles);
    }

}