using Microsoft.EntityFrameworkCore;
using WebAppTest3.Models;

namespace WebAppTest3.DAL
{
    public class AppDbContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions opt):base(opt){ }
    }
}
