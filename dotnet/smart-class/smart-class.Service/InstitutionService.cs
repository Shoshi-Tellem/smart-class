using smart_class.Core.Entities;
using smart_class.Core.Repositories;
using smart_class.Core.Services;

namespace smart_class.Service
{
    public class InstitutionService(IRepositoryManager repositoryManager) : IInstitutionService
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IRepository<Institution> _institutionRepository = repositoryManager.InstitutionRepository;

        public async Task<IEnumerable<Institution>> GetInstitutionsAsync()
        {
            return await _institutionRepository.GetAsync();
        }
        public async Task<Institution?> GetInstitutionByIdAsync(int id)
        {
            return await _institutionRepository.GetByIdAsync(id);
        }
        public async Task<Institution> AddInstitutionAsync(Institution institution)
        {
            Institution addedInstitution = await _institutionRepository.AddAsync(institution);
            await _repositoryManager.SaveAsync();
            return addedInstitution;
        }
        public async Task<Institution?> UpdateInstitutionAsync(int id, Institution institution)
        {
            Institution? existingInstitution = await GetInstitutionByIdAsync(id);
            if (existingInstitution == null)
                return null;
            existingInstitution.Name = institution.Name;
            Institution updatedInstitution = await _institutionRepository.UpdateAsync(existingInstitution);
            await _repositoryManager.SaveAsync();
            return updatedInstitution;
        }
        public async Task<Institution?> DeleteAsync(int id)
        {
            Institution? existingInstitution = await GetInstitutionByIdAsync(id);
            if (existingInstitution == null)
                return null;
            Institution? deletedInstitution = await _institutionRepository.DeleteAsync(existingInstitution);
            await _repositoryManager.SaveAsync();
            return deletedInstitution;
        }
    }
}