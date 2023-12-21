using MediatR;
using n5.Application.DTOs;


namespace n5.Application.Queries
{
    public record GetPermissionsQuery(bool SearchTerm = false) : IRequest<IEnumerable<PermissionDto>>;
    
}
