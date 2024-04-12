using System.Security.Claims;
using asp.net.Students.Areas.Auth.Models;
using asp.net.Students.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace asp.net.Students.Areas.Auth.Controllers;

[Area("Auth")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    public AccountController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Register(string? returnUrl)
    {
        return View(new RegisterForm());
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterForm form, string? returnUrl)
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

        await SignIn(user);

        await _userManager.AddToRoleAsync(user, "Guest");
        if (returnUrl != null)
        {
            return Redirect(returnUrl);
        }

        return Redirect("/");
    }
    
    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl)
    {
        return View(new LoginForm());
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginForm form, string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }
        
        var user = await _userManager.FindByEmailAsync(form.Login);
        if (user==null)
        {
            ModelState.AddModelError(key:nameof(form.Login), "User with such email not found");
            return View(form);
        }

        if (!await _userManager.CheckPasswordAsync(user, form.Password))
        {
            ModelState.AddModelError(key:nameof(form.Password), "Invalid password");
            return View(form);
        }

        await SignIn(user);

        if (returnUrl != null)
        {
            return Redirect(returnUrl);
        }

        return Redirect("/");
    }

    private async Task SignIn(User user)
    {
        var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        
        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var userRole in userRoles)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, userRole));
        }
        
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        return Redirect("/");
    }
    
    
}