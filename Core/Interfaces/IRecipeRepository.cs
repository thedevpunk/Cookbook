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
    }
}