using n5.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n5.Domain.Interfaces
{
    public interface IPermissionRepository
    {
        Task<Permission> GetPermissionAsync(int id);
        Task AddPermissionAsync(Permission permission);
        Task UpdatePermissionAsync(Permission permission);
        
    }
}
