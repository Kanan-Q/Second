namespace WebAppTest3.ViewModels.Employees
{
    public class EmployeeCreateVM
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public IFormFile Photo { get; set; }
        public string? Comment { get; set; }
        public int? DepartmentId { get; set; }
    }
}
