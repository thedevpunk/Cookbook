using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infastructure.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CookbookContext _context;

        public CategoryRepository(CookbookContext context)
        {
            _context = context;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await _context.Categories.InsertOneAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var filter = Builders<Category>.Filter.Eq(e => e.Id, id);

            await _context.Categories.DeleteOneAsync(filter);
        }

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.FindSync(new BsonDocument()).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            var filter = Builders<Category>.Filter.Eq(e => e.Id, id);

            return await _context.Categories.FindSync(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var filter = Builders<Category>.Filter.Eq(e => e.Id, category.Id);

            await _context.Categories.ReplaceOneAsync(filter, category, new ReplaceOptions{ IsUpsert = true }); 
        }
    }
}