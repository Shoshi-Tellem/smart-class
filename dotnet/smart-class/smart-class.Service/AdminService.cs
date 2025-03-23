using smart_class.Core.Entities;
using smart_class.Core.Repositories;
using smart_class.Core.Services;

namespace smart_class.Service
{
    public class AdminService(IRepositoryManager repositoryManager) : IAdminService
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IRepository<Admin> _adminRepository = repositoryManager.AdminRepository;

        public async Task<IEnumerable<Admin>> GetAdminsAsync()
        {
            return await _adminRepository.GetAsync();
        }
        public async Task<Admin?> GetAdminByIdAsync(int id)
        {
            return await _adminRepository.GetByIdAsync(id);
        }
        public async Task<Admin> AddAdminAsync(Admin admin)
        {
            Admin addedAdmin = await _adminRepository.AddAsync(admin);
            await _repositoryManager.SaveAsync();
            return addedAdmin;
        }
        public async Task<Admin?> UpdateAdminAsync(int id, Admin admin)
        {
            Admin? existingAdmin = await GetAdminByIdAsync(id);
            if (existingAdmin == null)
                return null;
            existingAdmin.Name = admin.Name;
            existingAdmin.Email = admin.Email;
            existingAdmin.UpdatedAt = DateTime.Now;
            Admin updatedAdmin = await _adminRepository.UpdateAsync(existingAdmin);
            await _repositoryManager.SaveAsync();
            return updatedAdmin;
        }
        public async Task<Admin?> DeleteAsync(int id)
        {
            Admin? existingAdmin = await GetAdminByIdAsync(id);
            if (existingAdmin == null)
                return null;
            Admin? deletedAdmin = await _adminRepository.DeleteAsync(existingAdmin);
            await _repositoryManager.SaveAsync();
            return deletedAdmin;
        }
    }
}
