using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupByIdAsync(Guid id);

        Task<IReadOnlyList<Group>> GetGroupsByIds(List<Guid> ids);

        Task<IReadOnlyList<Group>> GetGroupsAsync();

         Task CreateGroupAsync(Group group);

         Task UpdateGroupAsync(Group group);

         Task DeleteGroupAsync(Guid id);

         Task AddAdminAsync(Guid id, Guid adminId);

         Task RemoveAdminAsync(Guid id, Guid adminId);

         Task AddUserAsync(Guid id, Guid userId);

         Task RemoveUserAsync(Guid id, Guid userId);

         Task AddRecipeAsync(Guid id, Guid recipeId);

         Task RemoveRecipeAsync(Guid id, Guid recipeId);
    }
}