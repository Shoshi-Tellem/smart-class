using smart_class.Core.Entities;
using smart_class.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace smart_class.Data.Repositories
{
    public class AdminRepository(DataContext context) : IAdminRepository
    {
        private readonly DataContext _context = context;

        public async Task<IEnumerable<Admin>> GetAdminsAsync()
        {
            return await _context.Admin.ToListAsync();/*.Include(a=>a.Institution);*/
        }

        public async Task<Admin?> GetAdminByIdAsync(int id)
        {
            return await _context.Admin.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Admin> AddAdminAsync(Admin admin)
        {
            await _context.AddAsync(admin);
            return admin;
        }

        public async Task<Admin> UpdateAdminAsync(Admin admin)
        {
            _context.Update(admin);
            return admin;
        }

        public async Task<Admin?> DeleteAdminAsync(Admin admin)
        {
            _context.Admin.Remove(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
    }
}