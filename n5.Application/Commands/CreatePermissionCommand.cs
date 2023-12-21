using MediatR;
using n5.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n5.Application.Commands
{
    public record CreatePermissionCommand(string EmployeeForeName, string EmployeeSureName, 
        int IdPermissionType, DateTime PermissionDate) : IRequest<PermissionDto>;
    
}
