using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppTest3.DAL;
using WebAppTest3.Models;
using WebAppTest3.ViewModels.Departments;
namespace WebAppTest3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateVM vm)
        {
            Department e = new Department()
            {
                Name = vm.Name,
            };
            await _context.Departments.AddAsync(e);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data= await _context.Departments.Where(x=>x.Id==id).Select(x=>new DepartmentUpdateVM
            {
                Name=x.Name,
            }).FirstOrDefaultAsync();
            if(data is null) return NotFound();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,DepartmentUpdateVM vm)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Departments.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data is null) return NotFound();
            data.Name=vm.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var data = await _context.Departments.FindAsync(id);
            if(data is null) return NotFound();
            _context.Departments.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Departments.FindAsync(id);
            if (data is null) return NotFound();
            data.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Departments.FindAsync(id);
            if (data is null) return NotFound();
            data.IsDeleted = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
