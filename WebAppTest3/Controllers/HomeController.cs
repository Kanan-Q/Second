using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppTest3.Models;

namespace WebAppTest3.Controllers
{
    public class HomeController : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            return View();
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
