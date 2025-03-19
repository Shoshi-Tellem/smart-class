using smart_class.Core.Entities;
using smart_class.Core.Repositories;
using smart_class.Core.Services;

namespace smart_class.Service
{
    public class GroupService : IGroupService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IRepository<Group> _groupRepository;

        public GroupService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _groupRepository = repositoryManager.GroupRepository;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await _groupRepository.GetAsync();
        }

        public async Task<Group?> GetGroupByIdAsync(int id)
        {
            return await _groupRepository.GetByIdAsync(id);
        }

        public async Task<Group> AddGroupAsync(Group group)
        {
            Group addedGroup = await _groupRepository.AddAsync(group);
            await _repositoryManager.SaveAsync();
            return addedGroup;
        }

        public async Task<Group?> UpdateGroupAsync(int id, Group group)
        {
            Group? existingGroup = await GetGroupByIdAsync(id);
            if (existingGroup == null)
                return null;

            existingGroup.Name = group.Name; // Assuming Group has a Name property
            Group updatedGroup = await _groupRepository.UpdateAsync(existingGroup);
            await _repositoryManager.SaveAsync();
            return updatedGroup;
        }

        public async Task<Group?> DeleteAsync(int id)
        {
            Group? existingGroup = await GetGroupByIdAsync(id);
            if (existingGroup == null)
                return null;

            Group? deletedGroup = await _groupRepository.DeleteAsync(existingGroup);
            await _repositoryManager.SaveAsync();
            return deletedGroup;
        }
    }
}