using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MongoDB.Bson;
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

        public async Task AddStepAsync(Guid id, Step step)
        {
            var filter = Builders<Recipe>.Filter.Eq(e => e.Id, id);

            var update = Builders<Recipe>.Update.Push<Step>(e => e.Steps, step);

            await _context.Recipes.UpdateOneAsync(filter, update);
        }

        public async Task AddTagAsync(Guid id, Core.Entities.Tag tag)
        {
            var filter = Builders<Recipe>.Filter.Eq(e => e.Id, id);

            var update = Builders<Recipe>.Update.Push<Core.Entities.Tag>(e => e.Tags, tag);

            await _context.Recipes.UpdateOneAsync(filter, update);
        }

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            await _context.Recipes.InsertOneAsync(recipe);
        }

        public async Task DeleteRecipeAsync(Guid id)
        {
            var filter = Builders<Recipe>.Filter.Eq(e => e.Id, id);

            await _context.Recipes.DeleteOneAsync(filter);
        }

        public async Task<Recipe> GetRecipeByIdAsync(Guid id)
        {
            var filter = Builders<Recipe>.Filter.Eq(e => e.Id, id);

            return await _context.Recipes.FindSync(filter).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Recipe>> GetRecipesAsync()
        {
            return await _context.Recipes.FindSync(new BsonDocument()).ToListAsync();
        }

        public async Task<Recipe> GetRecipeByUserIdAsync(Guid id, Guid userId)
        {
            var filterUser = Builders<User>.Filter.Eq(e => e.Id, userId);

            var user = await _context.Users.FindSync(filterUser).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("User does not exists.");
            }

            var filterGroups = Builders<Group>.Filter.In(e => e.Id, user.GroupIds);

            var groups = await _context.Groups.FindSync(filterGroups).ToListAsync();

            foreach (Group group in groups)
            {
                var recipeId = group.RecipeIds.FirstOrDefault(r => r == id);
                if (recipeId != null)
                {
                    return await GetRecipeByIdAsync(recipeId);
                }
            }

            return null;
        }

        public async Task<IReadOnlyList<Recipe>> GetRecipesByUserIdAsync(Guid userId)
        {
            var filterUser = Builders<User>.Filter.Eq(e => e.Id, userId);

            var user = await _context.Users.FindSync(filterUser).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("User does not exitst.");
            }

            var filterGroups = Builders<Group>.Filter.In(e => e.Id, user.GroupIds);

            var groups = await _context.Groups.FindSync(filterGroups).ToListAsync();

            var recipeIds = new List<Guid>();
            groups.ForEach(e => recipeIds.AddRange(e.RecipeIds));

            return await GetRecipesByIdsAsync(recipeIds);
        }

        public async Task<IReadOnlyList<Recipe>> GetRecipesByIdsAsync(List<Guid> ids)
        {
            var filter = Builders<Recipe>.Filter.In(e => e.Id, ids);

            return await _context.Recipes.FindSync(filter).ToListAsync();
        }

        public async Task RemoveIngredientAsync(Guid id, Ingredient ingredient)
        {
            var filter = Builders<Recipe>.Filter.Eq(e => e.Id, id);

            var update = Builders<Recipe>.Update.Pull<Ingredient>(e => e.Ingredients, ingredient);

            await _context.Recipes.UpdateOneAsync(filter, update);
        }

        public async Task RemoveStepAsync(Guid id, Step step)
        {
            var filter = Builders<Recipe>.Filter.Eq(e => e.Id, id);

            var update = Builders<Recipe>.Update.Pull<Step>(e => e.Steps, step);

            await _context.Recipes.UpdateOneAsync(filter, update);
        }

        public async Task RemoveTagAsync(Guid id, Core.Entities.Tag tag)
        {
            var filter = Builders<Recipe>.Filter.Eq(e => e.Id, id);

            var update = Builders<Recipe>.Update.Pull<Core.Entities.Tag>(e => e.Tags, tag);

            await _context.Recipes.UpdateOneAsync(filter, update);
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            var filter = Builders<Recipe>.Filter.Eq(e => e.Id, recipe.Id);

            await _context.Recipes.ReplaceOneAsync(filter, recipe, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var filter = Builders<Recipe>.Filter.Eq(e => e.Id, id);

            var recipe = await _context.Recipes.Find(filter).FirstOrDefaultAsync();

            return recipe == null ? false : true;
        }
    }
}