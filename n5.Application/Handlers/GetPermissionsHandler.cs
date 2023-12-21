using MediatR;
using Microsoft.EntityFrameworkCore;
using n5.Application.DTOs;
using n5.Application.Queries;
using n5.Domain.Entities;
using n5.Infrastructure;
using Nest;

namespace n5.Application.Handlers
{
    public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, IEnumerable<PermissionDto>>
    {

        private readonly n5DbContext _dbContext;
        private readonly IElasticClient _elasticClient;
        public GetPermissionsHandler(n5DbContext dbContext, IElasticClient elasticClient)
        {
            _dbContext = dbContext;
            _elasticClient = elasticClient;
        }
       
        public async Task<IEnumerable<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            // Supongamos que "request" tiene un campo opcional "SearchTerm".
            if ((request.SearchTerm))
            {
                // Realizar búsqueda en Elasticsearch
                var searchResponse = await _elasticClient.SearchAsync<Permission>(s => s
                    .Query(q => q.MatchAll())
                );

                if (!searchResponse.IsValid)
                {
                    // Manejar error o devolver una lista vacía
                    return Enumerable.Empty<PermissionDto>();
                }

                return searchResponse.Documents.Select(permission => new PermissionDto
                {
                    // Mapeo de campos...
                });
            }
            else
            {
                // Usar _dbContext para obtener todos los registros
                var permissions = await _dbContext.Permissions.ToListAsync(cancellationToken);

                return permissions.Select(permission => new PermissionDto
                {
                    Id = permission.Id,
                    EmployeeForeName = permission.EmployeeForeName,
                    EmployeeSureName = permission.EmployeeSureName,
                    PermissionDate = permission.PermissionDate,
                    PermissionType = permission.PermissionType

                });
            }
        }       

    }
}
