using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAppTest3.DAL;
using WebAppTest3.Models;

namespace WebAppTest3.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        public async Task<IActionResult> About()
        {
            return View();
        }
        public async Task<IActionResult> Contact()
        {
            return View();
        }
        public async Task<IActionResult> Service()
        {
            return View();
        }

    }
}
