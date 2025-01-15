using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppTest3.DAL;
using WebAppTest3.Models;
using WebAppTest3.ViewModels.Departments;
using WebAppTest3.ViewModels.Employees;

namespace WebAppTest3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {

        public async Task<IActionResult> Index()
        {

            return View(await _context.Employees.Include(x => x.Department).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVM vm)
        {

            if (!vm.Photo.ContentType.StartsWith("image"))
            {
                ModelState.AddModelError("Photo", "image deyil");
            }
            if (vm.Photo.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("Photo", "5mb dan coxdu");
                return View();
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
                return View();
            }
            string filename = Path.GetRandomFileName() + Path.GetExtension(vm.Photo.FileName);

            using (Stream s = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", "Employee", filename)))
            {
                await vm.Photo.CopyToAsync(s);
            }
            Employee e = new Employee()
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Comment = vm.Comment,
                Photo = filename,
                DepartmentId = vm.DepartmentId,
            };
            await _context.Employees.AddAsync(e);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
            if (!id.HasValue) return BadRequest();
            var data = await _context.Employees.Where(x => x.Id == id).Select(x => new EmployeeUpdateVM
            {
                Name = x.Name,
                Surname = x.Surname,
                Comment = x.Comment,
                DepartmentId= x.DepartmentId,
                
            }).FirstOrDefaultAsync();
            if (data is null) return NotFound();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, EmployeeUpdateVM vm)
        {

            if (!id.HasValue) return BadRequest();

            if (vm.Photo != null)
            {

                if (!vm.Photo.ContentType.StartsWith("image"))
                {
                    ModelState.AddModelError("Photo", "image deyil");
                }
                if (vm.Photo.Length < 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("Photo", "5mb dan coxdu");
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
                return View();

            }

            var data = await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (data is null) return NotFound();
            if (vm.Photo != null)
            {

                string oldname = Path.Combine(_env.WebRootPath, "imgs", "Employee", data.Photo);


                using (Stream s = System.IO.File.Create(oldname))
                {
                    await vm.Photo!.CopyToAsync(s);
                }
            }
            data.Name = vm.Name;
            data.Surname = vm.Surname;
            data.Comment = vm.Comment;
            data.DepartmentId = vm.DepartmentId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Employees.FindAsync(id);
            if (data is null) return NotFound();
            _context.Employees.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Employees.FindAsync(id);
            if (data is null) return NotFound();
            data.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Employees.FindAsync(id);
            if (data is null) return NotFound();
            data.IsDeleted = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
