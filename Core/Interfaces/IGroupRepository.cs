using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupByIdAsync(Guid id);

        Task<IReadOnlyList<Group>> GetGroupsByUserId(Guid id);

        Task<IReadOnlyList<Group>> GetGroupsAsync();

         Task CreateGroupAsync(Group group);

         Task UpdateGroupAsync(Group group);

         Task DeleteGroupAsync(Guid id);

         Task AddAdminAsync(Guid id, User admin);

         Task RemoveAdminAsync(Guid id, User admin);

         Task AddUserAsync(Guid id, User user);

         Task RemoveUserAsync(Guid id, User user);

         Task AddRecipeAsync(Guid id, Recipe recipe);

         Task RemoveRecipeAsync(Guid id, Recipe recipe);
    }
}