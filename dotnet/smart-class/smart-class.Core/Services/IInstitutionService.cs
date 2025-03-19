using smart_class.Core.Entities;

namespace smart_class.Core.Services
{
    public interface IInstitutionService
    {
        Task<IEnumerable<Institution>> GetInstitutionsAsync();
        Task<Institution?> GetInstitutionByIdAsync(int id);
        Task<Institution> AddInstitutionAsync(Institution institution);
        Task<Institution?> UpdateInstitutionAsync(int id, Institution institution);
        Task<Institution?> DeleteAsync(int id);
    }
}