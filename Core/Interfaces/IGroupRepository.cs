using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupByIdAsync(Guid id);

        Task<IReadOnlyList<Group>> GetGroupsByUserIdAsync(Guid userId);

        Task CreateGroupAsync(Group group);

        Task UpdateGroupAsync(Group group);

        Task DeleteGroupAsync(Guid id);

        Task AddUser(Guid userId);

        Task RemoveUser(Guid userId);

        Task AddAdmin(Guid userId);

        Task RemoveAdmin(Guid userId);

        Task AddRecipe(Guid recipeId);

        Task RemoveRecipe(Guid recipeId);
    }
}