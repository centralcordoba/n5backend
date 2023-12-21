using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n5.Application.DTOs
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string EmployeeForeName { get; set; }
        public string EmployeeSureName { get; set; }
        public int PermissionType { get; set; }
        public DateTime PermissionDate { get; set; }
        public bool ElasticSearh    { get; set; }
    }

}
