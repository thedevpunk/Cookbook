using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetRecipeByIdAsync(Guid id);

        Task<IReadOnlyList<Recipe>> GetRecipesByGroupIdAsync(Guid groupId);

        Task<IReadOnlyList<Recipe>> GetRecipesAsync();

        Task CreateRecipeAsync(Recipe recipe);

        Task UpdateRecipeAsync(Recipe recipe);

        Task DeleteRecipeAsync(Guid id);

        Task AddIngredient(Ingredient ingredient);

        Task RemoveIngredient(Ingredient ingredient);

        Task AddStep(Step step);

        Task RemoveStep(Step step);

        Task AddTag(Tag tag);

        Task RemoveTag(Tag tag);
    }
}