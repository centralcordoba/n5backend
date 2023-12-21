using MediatR;
using n5.Application.Commands;
using n5.Application.DTOs;
using n5.Infrastructure;


namespace n5.Application.Handlers
{
    public record UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, PermissionDto>
    {
        private readonly n5DbContext _dbContext;

        public UpdatePermissionHandler(n5DbContext dbContext)
        {
            _dbContext = dbContext;
         }

        public async Task<PermissionDto> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionItem = await _dbContext.Permissions.FindAsync(
                new object[] {request.Id }, cancellationToken);
            if (permissionItem == null)
            { 
                return null; 
            }

            permissionItem.EmployeeSureName = request.EmployeeSureName;
            permissionItem.EmployeeForeName = request.EmployeeForeName;
            permissionItem.IdPermissionType = request.IdPermissionType;
            permissionItem.PermissionDate = request.PermissionDate; 
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new PermissionDto
            {
                Id = permissionItem.Id,
                EmployeeForeName = permissionItem.EmployeeForeName,
                EmployeeSureName = permissionItem.EmployeeSureName,
                IdPermissionType = permissionItem.IdPermissionType,
                PermissionDate = permissionItem.PermissionDate
            };

        }
    }

}
