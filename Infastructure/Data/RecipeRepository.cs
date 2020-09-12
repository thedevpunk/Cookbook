using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MongoDB.Driver;

namespace Infastructure.Data
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CookbookContext _context;
        public RecipeRepository(CookbookContext context)
        {
            _context = context;
        }

        public async Task AddIngredientAsync(Guid id, Ingredient ingredient)
        {
            var filter = Builders<Recipe>.Filter.Eq(e => e.Id, id);

            var update = Builders<Recipe>.Update.Push<Ingredient>(e => e.Ingredients, ingredient);

            await _context.Recipes.UpdateOneAsync(filter, update);
        }

        public Task AddStepAsync(Guid id, Step step)
        {
            throw new NotImplementedException();
        }

        public Task AddTagAsync(Guid id, Core.Entities.Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task CreateRecipeAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRecipeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> GetRecipeByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Recipe>> GetRecipesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Recipe>> GetRecipesByGroupIdAsync(Guid groupId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveIngredientAsync(Guid id, Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public Task RemoveStepAsync(Guid id, Step step)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTagAsync(Guid id, Core.Entities.Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecipeAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}