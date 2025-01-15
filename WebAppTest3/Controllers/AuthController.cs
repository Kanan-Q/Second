using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppTest3.DAL;
using WebAppTest3.Models;

namespace WebAppTest3.Controllers
{
    public class AuthController(AppDbContext _context,UserManager<User> _u,SignInManager<User> _s) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _s.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

    }
}
