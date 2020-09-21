using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MongoDB.Driver;

namespace Infastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly CookbookContext _context;
        public UserRepository(CookbookContext context)
        {
            _context = context;
        }

        public async Task AddGroupToUser(Guid id, Guid groupId)
        {
            var filter = Builders<User>.Filter.Eq(e => e.Id, id);

            var update = Builders<User>.Update.Push<Guid>(e => e.GroupIds, groupId);

            await _context.Users.UpdateOneAsync(filter, update);
        }

        public async Task<User> GetUser(Guid id)
        {
            var filter = Builders<User>.Filter.Eq(e => e.Id, id);

            return await _context.Users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task RemoveGroupFromUser(Guid id, Guid groupId)
        {
            var filter = Builders<User>.Filter.Eq(e => e.Id, id);

            var update = Builders<User>.Update.Pull<Guid>(e => e.GroupIds, groupId);

            await _context.Users.UpdateOneAsync(filter, update);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var filter = Builders<User>.Filter.Eq(e => e.Id, id);

            var user = await _context.Users.Find(filter).FirstOrDefaultAsync();

            return user == null ? false : true;
        }
    }
}