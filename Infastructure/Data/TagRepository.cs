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

        public Task CreateTagAsync(Entity.Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTagAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Entity.Tag>> GetTagsAsync()
        {
            return await _context.Tags.FindSync(new BsonDocument()).ToListAsync();
        }

        public Task UpdateTagAsync(Entity.Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}