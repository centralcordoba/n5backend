
namespace n5.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string EmployeeForeName { get; set; }
        public string EmployeeSureName { get; set; }
        public int IdPermissionType { get; set; }
        public DateTime PermissionDate { get; set; }
        
    }

    
}
