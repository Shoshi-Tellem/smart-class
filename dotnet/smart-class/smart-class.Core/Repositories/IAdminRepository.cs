using smart_class.Core.Entities;

namespace smart_class.Core.Repositories
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAdminsAsync();
        Task<Admin?> GetAdminByIdAsync(int id);
        Task<Admin> AddAdminAsync(Admin admin);
        Task<Admin> UpdateAdminAsync(Admin admin);
        Task<Admin?> DeleteAdminAsync(Admin admin);
    }
}