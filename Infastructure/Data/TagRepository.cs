using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity = Core.Entities;
using Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infastructure.Data
{
    public class TagRepository : ITagRepository
    {
        private readonly CookbookContext _context;
        
        public TagRepository(CookbookContext context)
        {
            _context = context;
        }

        public async Task CreateTagAsync(Entity.Tag tag)
        {
            await _context.Tags.InsertOneAsync(tag);
        }

        public async Task DeleteTagAsync(Guid id)
        {
            var filter = Builders<Entity.Tag>.Filter.Eq(i => i.Id, id);

            await _context.Tags.DeleteOneAsync(filter);
        }

        public async Task<IReadOnlyList<Entity.Tag>> GetTagsAsync()
        {
            return await _context.Tags.FindSync(new BsonDocument()).ToListAsync();
        }
    }
}