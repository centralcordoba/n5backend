using MediatR;
using n5.Application.Commands;
using n5.Application.DTOs;
using n5.Domain.Entities;
using n5.Infrastructure;


namespace n5.Application.Handlers
{
    public class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand, PermissionDto>
    {

        private readonly n5DbContext _dbContext;

        public CreatePermissionHandler(n5DbContext dbContext)
        { 
            _dbContext = dbContext;
        }
        public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionItem = new Permission
            {
                EmployeeForeName = request.EmployeeForeName,
                EmployeeSureName = request.EmployeeSureName,
                PermissionType = request.PermissionType,
                PermissionDate = request.PermissionDate
            };

            _dbContext.Permissions.Add(permissionItem);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new PermissionDto
            {
                Id = permissionItem.Id,
                EmployeeForeName = permissionItem.EmployeeForeName,
                EmployeeSureName = permissionItem.EmployeeSureName,
                PermissionType = permissionItem.PermissionType,
                PermissionDate = permissionItem.PermissionDate
            };

        }
    }
}
