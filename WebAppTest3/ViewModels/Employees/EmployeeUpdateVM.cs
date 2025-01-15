namespace WebAppTest3.ViewModels.Employees
{
    public class EmployeeUpdateVM
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string CoverPhoto { get; set; }
        public IFormFile Photo { get; set; }
        public string? Comment { get; set; }
        public int? DepartmentId { get; set; }
    }
}
