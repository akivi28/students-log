using System.Security.Claims;
using asp.net.Students.Areas.Auth.Models;
using asp.net.Students.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User = asp.net.Students.Models.User;

namespace asp.net.Students.Areas.Auth.Controllers;

[Area("Auth")]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<Students.Models.User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public UserController(UserManager<Students.Models.User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _userManager.Users.ToListAsync());
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] RegisterForm form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        if (form.Password != form.ConfirmPassword)
        {
            ModelState.AddModelError(key:nameof(form.ConfirmPassword), "Passwords must match");
            return View(form);
        }

        Students.Models.User user = await _userManager.FindByEmailAsync(form.Login);
        if (user!=null)
        {
            ModelState.AddModelError(key:nameof(form.Login), "User with such email already exist");
            return View(form);
        }

        user = new User
        {
            Email = form.Login,
            UserName = form.Login,
        };

        var result = await _userManager.CreateAsync(user, form.Password);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(key:nameof(form.Password), "Password does not meet the security requirements");
            return View(form);
        }
        
        return Redirect("/Auth/User/Index");
    }
    
    
    [HttpGet] 
    public async Task<IActionResult>  Edit(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        ViewData["user"] = user;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] RegisterForm form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }
        if (form.Password != form.ConfirmPassword)
        {
            ModelState.AddModelError(key:nameof(form.ConfirmPassword), "Passwords must match");
            return View(form);
        }
        var user = await _userManager.FindByEmailAsync(form.Login);
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, resetToken, form.Password);
        
        if (!result.Succeeded)
        {
            ModelState.AddModelError(key:nameof(form.Password), "Password does not meet the security requirements");
            return View(form);
        }
        
        return Redirect("/Auth/User/Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        ViewData["UserRoles"] = await _userManager.GetRolesAsync(user);
        return PartialView("_UserDetailsPartial", user);
    }

    
    [HttpPost]
    public async Task<IActionResult> Delete(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        await _userManager.DeleteAsync(user);
        return Redirect("Index");
    }
    [HttpGet]
    public async Task<IActionResult> ChangeRole(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
        var userRoles = await _userManager.GetRolesAsync(user);

        ViewBag.UserId = userId;
        ViewBag.UserName = user.UserName;
        ViewBag.AllRoles = allRoles;
        ViewBag.UserRoles = userRoles;

        return PartialView("_ChangeRolePartial");
    }

    [HttpPost]
    public async Task<IActionResult> ChangeRole(string userId, string[] selectedRoles)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var userRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, userRoles);
        await _userManager.AddToRolesAsync(user, selectedRoles);
        return Redirect("Index");
    }
    
    
}