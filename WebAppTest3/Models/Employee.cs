namespace WebAppTest3.Models
{
    public class Employee:BaseEntity
    {
        
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Photo {  get; set; } 
        public string? Comment { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }


    }
}
