using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infastructure.Data
{
    public class GroupRepository : IGroupRepository
    {
        private readonly CookbookContext _context;
        public GroupRepository(CookbookContext context)
        {
            _context = context;
        }

        public async Task AddAdminAsync(Guid id, Guid adminId)
        {
            // TODO: Check if admin is already a user in this group

            var filter = Builders<Group>.Filter.Eq(e => e.Id, id);

            var update = Builders<Group>.Update.Push<Guid>(e => e.AdminIds, adminId);

            await _context.Groups.UpdateOneAsync(filter, update);
        }

        public async Task AddRecipeAsync(Guid id, Guid recipeId)
        {
            var filter = Builders<Group>.Filter.Eq(e => e.Id, id);

            var update = Builders<Group>.Update.Push<Guid>(e => e.RecipeIds, recipeId);

            await _context.Groups.UpdateOneAsync(filter, update);
        }

        public async Task AddUserAsync(Guid id, Guid userId)
        {
            // Add userId to group
            var filter = Builders<Group>.Filter.Eq(e => e.Id, id);

            var update = Builders<Group>.Update.Push<Guid>(e => e.UserIds, userId);

            await _context.Groups.UpdateOneAsync(filter, update);

            // TODO: Should this logic remain in this repo?
            // Add groupId to user
            var filterUser = Builders<User>.Filter.Eq(e => e.Id, userId);

            var updateUser = Builders<User>.Update.Push<Guid>(e => e.GroupIds, id);

            await _context.Users.UpdateOneAsync(filterUser, updateUser);
        }

        public async Task CreateGroupAsync(Group group)
        {
            await _context.Groups.InsertOneAsync(group);
        }

        public async Task DeleteGroupAsync(Guid id)
        {
            var filter = Builders<Group>.Filter.Eq(e => e.Id, id);

            await _context.Groups.DeleteOneAsync(filter);
        }

        public async Task<Group> GetGroupByIdAsync(Guid id)
        {
            var filter = Builders<Group>.Filter.Eq(e => e.Id, id);

            return await _context.Groups.FindSync(filter).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Group>> GetGroupsAsync()
        {
            return await _context.Groups.FindSync(new BsonDocument()).ToListAsync();
        }

        public async Task<IReadOnlyList<Group>> GetGroupsByIds(List<Guid> ids)
        {
            var filter = Builders<Group>.Filter.In(e => e.Id, ids);

            return await _context.Groups.Find(filter).ToListAsync();
        }

        public async Task RemoveAdminAsync(Guid id, Guid adminId)
        {
            var filter = Builders<Group>.Filter.Eq(e => e.Id, id);

            var update = Builders<Group>.Update.Pull<Guid>(e => e.AdminIds, adminId);

            await _context.Groups.UpdateOneAsync(filter, update);
        }

        public async Task RemoveRecipeAsync(Guid id, Guid recipeId)
        {
            var filter = Builders<Group>.Filter.Eq(e => e.Id, id);

            var update = Builders<Group>.Update.Pull<Guid>(e => e.RecipeIds, recipeId);

            await _context.Groups.UpdateOneAsync(filter, update);
        }

        public async Task RemoveUserAsync(Guid id, Guid userId)
        {
            // Remove userId from group
            var filter = Builders<Group>.Filter.Eq(e => e.Id, id);

            var update = Builders<Group>.Update.Pull<Guid>(e => e.UserIds, userId);

            await _context.Groups.UpdateOneAsync(filter, update);

            // TODO: Should this logic remain in this repo?
            // Remove groupId from user
            var filterUser = Builders<User>.Filter.Eq(e => e.Id, userId);

            var updateUser = Builders<User>.Update.Pull<Guid>(e => e.GroupIds, id);

            await _context.Users.UpdateOneAsync(filterUser, updateUser);
        }

        public async Task UpdateGroupAsync(Group group)
        {
            var filter = Builders<Group>.Filter.Eq(e => e.Id, group.Id);

            await _context.Groups.ReplaceOneAsync(filter, group);
        }
    }
}