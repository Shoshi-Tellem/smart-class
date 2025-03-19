using smart_class.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smart_class.Core.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<Group?> GetGroupByIdAsync(int id);
        Task<Group> AddGroupAsync(Group group);
        Task<Group?> UpdateGroupAsync(int id, Group group);
        Task<Group?> DeleteAsync(int id);
    }
}