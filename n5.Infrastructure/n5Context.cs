using Microsoft.EntityFrameworkCore;
using n5.Domain.Entities;
using n5.Domain.Interfaces;


namespace n5.Infrastructure
{
    // MyAppContext.cs
    public class n5DbContext : DbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        public n5DbContext(DbContextOptions<n5DbContext> options) : base(options) { }
       
    }

    // PermissionRepository.cs
    public class PermissionRepository : IPermissionRepository
    {
        private readonly n5DbContext _context;

        public PermissionRepository(n5DbContext context)
        {
            _context = context;
        }

        public async Task<Permission> GetPermissionAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task AddPermissionAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
        }

        public async Task UpdatePermissionAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
        }

    }

}
