using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(Guid id);

        Task AddGroupToUser(Guid id, Guid groupId);

        Task RemoveGroupFromUser(Guid id, Guid groupId);

        Task<bool> ExistsAsync(Guid id);
    }
}