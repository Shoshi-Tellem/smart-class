using smart_class.Core.Entities;

namespace smart_class.Core.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<Admin>> GetAdminsAsync();
        Task<Admin?> GetAdminByIdAsync(int id);
        Task<Admin> AddAdminAsync(Admin admin);
        Task<Admin?> UpdateAdminAsync(int id, Admin admin);
        Task<Admin?> UpdateAdminPasswordAsync(int id, string password);
        Task<Admin?> DeleteAsync(int id);
    }
}
